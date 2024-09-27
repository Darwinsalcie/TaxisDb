
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.UserGroup;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup, int>
    {
        /// <summary>
        /// Get All UserGroup Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserGroupModel>>> GetUserGroup();

        /// <summary>
        /// Get UserGroup  by name Method
        /// </summary>
        /// <param UGName="UGNname"></param>
        /// <returns></returns>
        Task<DataResults<UserGroupModel>> GetUserGroupbyName(string UGName);

    }
}
