using TaxisDb.Web.Models.BaseDTO;

namespace TaxisDb.Web.Models.Role
{
    public record RoleUpdateDTO : BaseUpdateEntityDTO
    {
        public string Rolename { get; set; }
        public string? Description { get; set; }

    }
}
