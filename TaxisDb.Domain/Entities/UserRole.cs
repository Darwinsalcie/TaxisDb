

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class UserRole : AuditEntity<int>
    {
        public override int Id { get ; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
