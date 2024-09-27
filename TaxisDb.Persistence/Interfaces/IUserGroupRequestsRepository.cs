
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.UserGroupRequests;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IUserGroupRequestsRepository : IRepository<UserGroupRequests, int>
    {
        /// <summary>
        /// Get All UserGroupRequest Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserGroupRequestsModel>>> GetUserGroupRequests();

        /// <summary>
        /// Get UserGroupRequests by name Method
        /// </summary>
        /// <param UGRName="UGRName"></param>
        /// <returns></returns>
        Task<DataResults<UserGroupRequestsModel>> GetUserGroupRequestsbyName (string UGRName);

    }
}
