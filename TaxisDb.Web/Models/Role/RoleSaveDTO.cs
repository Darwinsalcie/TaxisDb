using TaxisDb.Web.Models.BaseDTO;

namespace TaxisDb.Web.Models.Role
{
    public record RoleSaveDTO : BaseSaveEntityDTO
    {


        public string Rolename { get; set; }
        public string? Description { get; set; }


    }
}
