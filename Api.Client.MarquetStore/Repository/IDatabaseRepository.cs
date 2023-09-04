using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Client.MarquetStore.Repository
{
    public interface IDatabaseRepository
    {
        Task<IDbContextTransaction> BeginTransaction();
    }
}
