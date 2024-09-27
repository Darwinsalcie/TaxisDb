

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public  class Role : AuditEntity<int>
    {
        public override int Id { get; set ; }
        public string Rolename { get; set; }
        public string? Description { get; set; }

    }
}
