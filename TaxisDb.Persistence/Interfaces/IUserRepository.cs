
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.User;

namespace TaxisDb.Persistence.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        /// <summary>
        /// Get All Users Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserModel>>> GetUser();

        /// <summary>
        /// Get user by name Method
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DataResults<UserModel>> GetUserbyName(string name);
        /// <summary>
        /// Get user by usergroup method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<UserModel>>> GetUserByUserGroup(int usergroupId);

        /// <summary>
        /// Get User by UserId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResults<UserModel>> GetUserById(int Id);

    }
}
