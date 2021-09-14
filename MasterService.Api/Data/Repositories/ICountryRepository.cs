using MasterService.Api.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterService.Api.Data.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> AddAsync(Country country);

        Task<bool> UpdateAsync(Country country);

        Task<bool> DeleteAsync(int id, string cluster);

        Task<Country> GetAsync(int id);

        Task<List<Country>> GetAllAsync();

        Task<List<Country>> GetAllAsync(string cluster);
    }
}
