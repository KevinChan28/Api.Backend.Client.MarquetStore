using Api.Client.MarquetStore.DTO;

namespace Api.Client.MarquetStore.Service
{
    public interface ISend
    {
        Task<bool> SendEmail(EmailDTO model);
    }
}
