using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IProductsRepository
    {
        Task<Product> GetProductById(int idProduct);
        Task<int> Update(Product product);
    }
}
