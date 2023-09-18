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

        public async Task<ViewPrincipalProducts> GetProducts()
        {
            List<Product>getProducts = await _productsRepository.GetProducts();
            ViewPrincipalProducts products = new ViewPrincipalProducts
            {
                TotalProduct = getProducts.Count(),
                Products = getProducts
            };

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
