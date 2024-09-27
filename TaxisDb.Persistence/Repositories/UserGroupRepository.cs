using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.UserGroup;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class UserGroupRepository : BaseRepository<UserGroup, int>, IUserGroupRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<UserGroup> logger;
        private readonly IConfiguration configuration;
        public UserGroupRepository(Taxisdb taxisdb,
                               ILogger<UserGroup> logger,
                               IConfiguration configuration,
                               UserGroupValidator userGroupValidator) : base(taxisdb, userGroupValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<UserGroupModel>>> GetUserGroup()
        {
            DataResults<List<UserGroupModel>> result = new DataResults<List<UserGroupModel>>();

            try
            {
                var query = await GetUserGroupBaseQuery().ToListAsync();
                result.Result = query;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserGroup:get_groups"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroup:get_groups"], ex.ToString());
            }

            return result;
        }

        public async Task<DataResults<UserGroupModel>> GetUserGroupbyName(string UGName)
        {
            DataResults<UserGroupModel> result = new DataResults<UserGroupModel>();

            try
            {
                UserGroupModel? userGroup = await GetUserGroupBaseQuery()
                    .Where(ug => ug.GroupName == UGName)
                    .FirstOrDefaultAsync();

                if (userGroup != null)
                {
                    result.Result = userGroup;
                }
                else
                {
                    result.Message = this.configuration["UserGroup:group_not_found"];
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserGroup:error_get_group"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroup:error_get_group"], ex.ToString());
            }

            return result;
        }


        public override async Task<bool> Save(UserGroup entity)
        {
            bool result = false;

            try
            {
                if (await base.Exists(ug => ug.Id == entity.Id))
                    throw new EntityDataException(this.configuration["UserGroup:id_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroup:error_save"], ex.ToString());
            }

            return result;
        }

        public override async Task<bool> Update(UserGroup entity)
        {
            bool result = false;

            try
            {
                UserGroup? userGroupToUpdate = this.taxisdb.UserGroup.Find(entity.Id);

                if (userGroupToUpdate == null)
                    throw new EntityDataException(this.configuration["UserGroup:not_found"]);

                userGroupToUpdate.GroupName = entity.GroupName;
                userGroupToUpdate.Description = entity.Description;
                userGroupToUpdate.ModifyDate = entity.ModifyDate;
                userGroupToUpdate.ModifyUser = entity.ModifyUser;

                result = await base.Update(userGroupToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroup:error_update"], ex.ToString());
            }

            return result;
        }

        private IQueryable<UserGroupModel> GetUserGroupBaseQuery()
        {
            return from userGroup in this.taxisdb.UserGroup
                   where userGroup.Deleted == false
                   select new UserGroupModel()
                   {
                       Id = userGroup.Id,
                       GroupName = userGroup.GroupName,
                       Description = userGroup.Description
                   };
        }
    }
}
