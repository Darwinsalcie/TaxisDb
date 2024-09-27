

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class UserGroup : AuditEntity<int>
    {
        public override int Id { get; set; }
        public string GroupName { get; set; }
        public string? Description { get; set; }
    }
}
