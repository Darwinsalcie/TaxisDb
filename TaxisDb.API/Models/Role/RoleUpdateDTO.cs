using TaxisDb.API.Models.BaseDTO;

namespace TaxisDb.API.Models.Role
{
    public record RoleUpdateDTO : BaseUpdateEntityDTO
    {
        public string Rolename { get; set; }
        public string? Description { get; set; }

    }
}
