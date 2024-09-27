

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class User : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int UserGroupId { get; set; }
        public int UserGroupReqId { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}
