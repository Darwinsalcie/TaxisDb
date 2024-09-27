using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.User;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<UserRepository> logger;
        private readonly IConfiguration configuration;
        public UserRepository(Taxisdb taxisdb,
                               ILogger<UserRepository> logger,
                               IConfiguration configuration,
                               UserValidator userValidator) : base(taxisdb, userValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<UserModel>>> GetUser()
        {
            DataResults<List<UserModel>> result = new DataResults<List<UserModel>>();
            try
            {
                var users = await GetUserBaseQuery().ToListAsync();
                result.Result = users;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["User:get_all_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["User:get_all_error"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<UserModel>> GetUserbyName(string name)
        {
            DataResults<UserModel> result = new DataResults<UserModel>();
            try
            {
                var user = await GetUserBaseQuery()
                                .Where(u => u.Nombre == name)
                                .FirstOrDefaultAsync();

                result.Result = user;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["User:get_by_name_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["User:get_by_name_error"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<List<UserModel>>> GetUserByUserGroup(int usergroupId)
        {
            DataResults<List<UserModel>> result = new DataResults<List<UserModel>>();

            try
            {
                var query = await GetUserBaseQuery()
                    .Where(user => user.UserGroupId == usergroupId)
                    .ToListAsync();
                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["User:get_taxi_user_id"];
                result.Success = false;
                this.logger.LogError(this.configuration["User:get_taxi_user_id"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Save(User entity)
        {
            bool result = false;
            try
            {
                if (await base.Exists(us => us.Id == entity.Id))
                    throw new EntityDataException(this.configuration["User:id_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["User:error_save"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Update(User entity)
        {
            bool result = false;
            try
            {
                User? userToUpdate = this.taxisdb.User.Find(entity.Id);

                if (userToUpdate == null)
                    throw new EntityDataException(this.configuration["User:not_found"]);

                userToUpdate.UserGroupId = entity.UserGroupId;
                userToUpdate.UserGroupReqId = entity.UserGroupReqId;
                userToUpdate.Documento = entity.Documento;
                userToUpdate.ModifyDate = entity.ModifyDate;
                userToUpdate.ModifyUser = entity.ModifyUser;
                userToUpdate.Nombre = entity.Nombre;
                userToUpdate.Apellido = entity.Apellido;

                result = await base.Update(userToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["User:error_update"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Remove(int id)
        {
            bool result = false;
            try
            {
                User? userToRemove = await this.taxisdb.User.FindAsync(id);
                if (userToRemove != null)
                {
                    userToRemove.Deleted = true; // Marcar como eliminado
                    result = await base.Update(userToRemove);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(this.configuration["User:error_remove"], ex.ToString());
            }
            return result;
        }
        private IQueryable<UserModel> GetUserBaseQuery()
        {
            return from user in this.taxisdb.User
                   join userGroup in this.taxisdb.UserGroup on user.UserGroupId equals userGroup.Id
                   join userGroupRequests in this.taxisdb.UserGroupRequests on user.UserGroupReqId equals userGroupRequests.Id
                   where user.Deleted != true
                   select new UserModel
                   {
                       Id = user.Id,
                       UserGroupId = userGroup.Id,
                       UserGroupReqId = userGroupRequests.Id,
                       Documento = user.Documento,
                       Nombre = user.Nombre,
                       Apellido = user.Apellido
                   };
        }
    }
}
