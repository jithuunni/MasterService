using System;

namespace MasterService.Api.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Cluster { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
