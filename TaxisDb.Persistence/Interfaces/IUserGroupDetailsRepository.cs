
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.UserGroupDetails;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IUserGroupDetailsRepository : IRepository<UserGroupDetails, int>
    {
        /// <summary>
        /// Get All Usersgroupdetails List Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserGroupDetailsModel>>> GetUserGroupDetails();

        /// <summary>
        /// Get Usergroupdetails by name Method
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DataResults<UserGroupDetailsModel>> GetUserGroupDetailsbyUser(string User);

    }
}
