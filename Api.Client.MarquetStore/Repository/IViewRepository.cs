namespace Api.Client.MarquetStore.Repository
{
    public interface IViewRepository
    {
        Task<string> GetHtmlWelcome(); 
        Task<string> GetHtmñRecoverPassword(); 
    }
}
