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


        public async Task<int> RegisterSale(SaleRegister model)
        {
            using (var trasaction = await _databaseRepository.BeginTransaction())
            {
                try
                {
                    int conceptoId = 0;
                    int idPersonalization = 0;

                    Product product = await _productsRepository.GetProductById(model.ProductId);

                    if (product == null)
                    {
                        return 0;
                    }

                    Sale sale = new Sale
                    {
                        CreatedDate = DateTime.Now,
                        IsDelivered = false,
                        UserId = model.UserId,
                        Total = model.Concepts.Sum(a => a.Quantity * product.Price)
                    };
                    int SaleId = await _saleRepository.Register(sale);

                    foreach (ConceptRegister requestConcept in model.Concepts)
                    {
                        Concept conceptNew = new Concept
                        {
                            Quantity = requestConcept.Quantity,
                            SaleId = sale.Id,
                            ProductId = model.ProductId,
                            Import = requestConcept.Quantity * product.Price,
                        };
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
