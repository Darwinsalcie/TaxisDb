using TaxisDb.Domain.Base;

namespace TaxisDb.Persistence.Models.Role
{
    public class RoleModel : AuditEntity<int>
    {
        public override int Id { get; set; }
        public string Rolename { get; set; }
        public string? Description { get; set; }
        //public DateTime CreationDate { get; set; }
    }
}
