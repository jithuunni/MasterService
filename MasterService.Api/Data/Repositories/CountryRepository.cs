using System.Linq;
using MasterService.Api.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterService.Api.Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(IConfiguration configuration) : base(configuration, "Country")
        {
        }

        public async Task<Country> AddAsync(Country country)
        {
            return await base.CreateAsync(country);
        }

        public async Task<bool> DeleteAsync(int id, string cluster)
        {
            return await base.RemoveAsync(id.ToString(), cluster);
        }

        public async Task<List<Country>> GetAllAsync()
        {
            string query = "SELECT * FROM c";
            return await base.ReadAsysnc(query);
        }

        public async Task<List<Country>> GetAllAsync(string cluster)
        {
            string query = $"SELECT * FROM c WHERE c.Cluster = '{cluster}'";
            return await base.ReadAsysnc(query);
        }

        public async Task<Country> GetAsync(int id)
        {
            string query = $"SELECT * FROM c WHERE c.id = '{id}'";
            var list = await base.ReadAsysnc(query);
            return list.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Country country)
        {
            return await base.ReplaceAsync(country, country.id);
        }
    }
}
