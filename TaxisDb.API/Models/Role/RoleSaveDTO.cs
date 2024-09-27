using TaxisDb.API.Models.BaseDTO;

namespace TaxisDb.API.Models.Role
{
    public record RoleSaveDTO : BaseSaveEntityDTO
    {


        public string Rolename { get; set; }
        public string? Description { get; set; }


    }
}
