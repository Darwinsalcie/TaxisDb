

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class UserGroupRequests : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int UserGroupId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
