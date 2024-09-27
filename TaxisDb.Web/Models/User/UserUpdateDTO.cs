using TaxisDb.Web.Models.BaseDTO;

namespace TaxisDb.Web.Models.User
{
    public record UserUpdateDTO : BaseUpdateEntityDTO
    {
        public int UserGroupId { get; set; }
        public int UserGroupReqId { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
