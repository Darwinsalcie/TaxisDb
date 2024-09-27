

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class UserGroupDetails : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int UserGroupId { get; set; }
    }
}
