using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpProductsService : IProductsService
    {
        IProductsRepository _productsRepository;

        public ImpProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<InformationProducts>> GetProducts()
        {
            List<Product>getProducts = await _productsRepository.GetProducts();
            List<InformationProducts> products = getProducts.Select(a => new InformationProducts
            {
               Name = a.Name,
               Description = a.Description,
               Id = a.Id,
               Price = a.Price,
            }).ToList();

            return products;
        }

        public async Task<int> RegisterProduct(ProductRegister model)
        {
            Product productNew = new Product
            {
                Name = model.Name,
                Description = model.Description,
                IsAvailable = model.IsAvailable,
                PathImage = model.Pathlmage,
                Price = model.Price,
                Stock = model.Stock
            };
            int idProduct = await _productsRepository.RegisterProduct(productNew);

            return idProduct;
        }
    }
}
