using Api.Client.MarquetStore.Context;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpViewRepository : IViewRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpViewRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetHtmlWelcome()
        {
            return _dbContext.Views.Where(x => x.Id == 1).Select(z => z.Content).FirstOrDefault();
        }

        public async Task<string> GetHtmñRecoverPassword()
        {
            return _dbContext.Views.Where(x => x.Id == 2).Select(z => z.Content).FirstOrDefault();
        }
    }
}
