
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.UserRole;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole, int>
    {
        /// <summary>
        /// Get All UserRole Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserRoleModel>>> GetUserRole();

        /// <summary>
        /// Get UserRole  by userId Method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DataResults<List<UserRoleModel>>> GetUserRolebyUserId(int userId);

        /// <summary>
        /// Get UserRole by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<DataResults<List<UserRoleModel>>> GetUserRolebyRoleId(int roleId);

    }
}
