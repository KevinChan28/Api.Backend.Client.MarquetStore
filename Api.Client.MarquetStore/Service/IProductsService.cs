using Api.Client.MarquetStore.DTO;

namespace Api.Client.MarquetStore.Service
{
    public interface IProductsService
    {
        Task<int> RegisterProduct(ProductRegister model);
        Task<ViewPrincipalProducts> GetProducts();
    }
}
