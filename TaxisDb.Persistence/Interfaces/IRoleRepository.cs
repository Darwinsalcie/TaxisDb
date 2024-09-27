

using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.Role;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        /// <summary>
        /// Get Roles List Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<RoleModel>>> GetRoles();

        /// <summary>
        /// Get Rol by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DataResults<RoleModel>> GetRoles(string name);

        Task<DataResults<RoleModel>> GetRolesById(int Id);



    }
}
