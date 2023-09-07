using Api.Client.MarquetStore.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Client.MarquetStore.Repository
{
    public interface IPersonalizationRepository
    {
        Task<int> Register(Personalization personalization);
        Task<List<Personalization>> GetAllPersonalizations();
    }
}
