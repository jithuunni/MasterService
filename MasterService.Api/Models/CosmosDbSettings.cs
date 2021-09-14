using System.Collections.Generic;

namespace MasterService.Api.Models
{
    public class CosmosDbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public IEnumerable<DbContainer> Containers { get; set; }
    }
}
