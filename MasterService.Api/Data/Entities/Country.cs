namespace MasterService.Api.Data.Entities
{
    public class Country : EntityBase
    {
        public string Cluster { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string CreatedBy { get; set; }
    }
}
