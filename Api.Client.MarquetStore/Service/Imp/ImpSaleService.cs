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

        public ImpSaleService(ISaleRepository saleServiceRepository, IConceptRepository conceptRepository,
            IProductsRepository productsRepository, IPersonalizationRepository personalizationRepository, IDatabaseRepository databaseRepository)
        {
            _saleRepository = saleServiceRepository;
            _conceptRepository = conceptRepository;
            _productsRepository = productsRepository;
            _personalizationRepository = personalizationRepository;
            _databaseRepository = databaseRepository;
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
            using (var trasaction = await _databaseRepository.BeginTransaction())
            {
                try
                {
                    int conceptoId = 0;
                    int idPersonalization = 0;
                    decimal totalSale = 0;

                    Sale sale = new Sale
                    {
                        CreatedDate = DateTime.Now,
                        IsDelivered = false,
                        UserId = model.UserId,
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
                        requestConcept.Import = conceptNew.Import;
                         conceptoId = await _conceptRepository.Register(conceptNew);

                        product.Stock -= conceptNew.Quantity;
                        int productUpdated = await _productsRepository.Update(product);

                        foreach (PersonalizationRegister item in requestConcept.Personalizations)
                        {
                            Personalization personalization = new Personalization
                            {
                                ConceptId = conceptoId,
                                IngredientId = item.IngredientId
                            };
                             idPersonalization = await _personalizationRepository.Register(personalization);
                        }
                    }

                    totalSale = model.Concepts.Sum(a => a.Import);
                    Sale saleUpdate = await _saleRepository.GetSaleById(SaleId);
                    saleUpdate.Total = totalSale;
                    await _saleRepository.UpdateSale(saleUpdate);

                    if (SaleId < 1 || conceptoId < 1 || idPersonalization < 1)
                    {
                        trasaction.Rollback();
                        return 0;
                    }

                    trasaction.Commit();
                    return SaleId;
                }
                catch (Exception)
                {

                    trasaction.Rollback();
                }

                return 0;
            }
        }
    }
}
