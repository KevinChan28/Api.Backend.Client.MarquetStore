using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpSaleService : ISaleService
    {
        ISaleRepository _saleRepository;
        IConceptRepository _conceptRepository;
        IProductsRepository _productsRepository;
        IPersonalizationRepository _personalizationRepository;
        IDatabaseRepository _databaseRepository;
        IIngredientsRepository _ingredientsRepository;

        public ImpSaleService(ISaleRepository saleServiceRepository, IConceptRepository conceptRepository,
            IProductsRepository productsRepository, IPersonalizationRepository personalizationRepository, IDatabaseRepository databaseRepository,
            IIngredientsRepository ingredientsRepository)
        {
            _saleRepository = saleServiceRepository;
            _conceptRepository = conceptRepository;
            _productsRepository = productsRepository;
            _personalizationRepository = personalizationRepository;
            _databaseRepository = databaseRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<List<SalesOfCustomer>> GetSalesOfCustomer(int idCustomer)
        {
            List<Sale> sales = await _saleRepository.GetSalesOfCustomer(idCustomer);
            List<Concept> concepts = await _conceptRepository.GetAllConcepts();
            List<Personalization> personalizations = await _personalizationRepository.GetAllPersonalizations();
            List<Product> products = await _productsRepository.GetProducts();
            List<SalesOfCustomer> salesOfCustomers = sales.Select(a => new SalesOfCustomer
            {
                IdSale = a.Id,
                CreatedDate = a.CreatedDate,
                Total = a.Total,
                IsDelivered = a.IsDelivered,
                Concepts = concepts.Select(x => new ConceptsOfCustomer
                {
                    ConceptId = x.Id,
                    Import = x.Import,
                    Product = products.Select(u => new InformationProducts
                    {
                        IdProduct = u.Id,
                        Name = u.Name,
                        Description = u.Description,
                        Price = u.Price
                    }).FirstOrDefault(),
                    Quantity = x.Quantity,
                    SaleId = x.SaleId,
                    Personalizations = personalizations.Select(x => new PersonalizationsOfCustomer
                    {
                        IdPersonalization= x.Id,
                        ConceptId = x.ConceptId,
                        Ingredients = x.IngredientId,
                    }).Where(z => z.ConceptId == x.Id).ToList()
                }).Where(c => c.SaleId == a.Id).ToList()
            }).ToList();

            return salesOfCustomers;
        }

        public async Task<int> RegisterSale(SaleRegister model)
        {
            using (var transaction = await _databaseRepository.BeginTransaction())
            {
                try
                {
                    int conceptoId = 0;
                    int idPersonalization = 0;
                    decimal totalIngredient = 0;

                    Sale sale = new Sale
                    {
                        CreatedDate = DateTime.Now,
                        IsDelivered = false,
                        UserId = model.UserId,
                        Total = model.Total
                    };
                    int SaleId = await _saleRepository.Register(sale);

                    foreach (ConceptRegister requestConcept in model.Concepts)
                    {
                        Product product = await _productsRepository.GetProductById(requestConcept.ProductId);
                        Concept conceptNew = new Concept
                        {
                            Quantity = requestConcept.Quantity,
                            SaleId = SaleId,
                            ProductId = requestConcept.ProductId,
                            Price = product.Price,
                            Import = requestConcept.Quantity * product.Price,
                        };
                        
                         conceptoId = await _conceptRepository.Register(conceptNew);

                        product.Stock -= conceptNew.Quantity;
                        int productUpdated = await _productsRepository.Update(product);

                        totalIngredient = 0;

                        foreach (PersonalizationRegister item in requestConcept.Personalizations)
                        {
                            Ingredient ingredient = await _ingredientsRepository.GetIngredientById(item.IngredientId);
                            Personalization personalization = new Personalization
                            {
                                ConceptId = conceptoId,
                                IngredientId = item.IngredientId
                            };
                             idPersonalization = await _personalizationRepository.Register(personalization);
                            totalIngredient = (conceptNew.Quantity * ingredient.Price) + totalIngredient;
                        }

                        Concept conceptFind = await _conceptRepository.GetConceptById(conceptoId);
                        conceptFind.Import += totalIngredient;
                        await _conceptRepository.Update(conceptFind);
                    }

                    if (SaleId < 1 || conceptoId < 1 || idPersonalization < 1)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                    transaction.Commit();
                    return SaleId;
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }

                return 0;
            }
        }
    }
}
