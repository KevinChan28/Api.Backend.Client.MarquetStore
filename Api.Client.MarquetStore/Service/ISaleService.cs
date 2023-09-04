using Api.Client.MarquetStore.DTO;

namespace Api.Client.MarquetStore.Service
{
    public interface ISaleService
    {
        Task<int> RegisterSale(SaleRegister model);
    }
}
