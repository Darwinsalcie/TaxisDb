using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Taxi;
using TaxisDb.Persistence.Models.UserGroupDetails;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class UserGroupDetailsRepository : BaseRepository<UserGroupDetails, int>, IUserGroupDetailsRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<UserGroupDetailsRepository> logger;
        private readonly IConfiguration configuration;
        public UserGroupDetailsRepository(Taxisdb taxisdb,
                               ILogger<UserGroupDetailsRepository> logger,
                               IConfiguration configuration,
                               UserGroupDetailsValidator userGroupDetailsValidator) : base(taxisdb, userGroupDetailsValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<UserGroupDetailsModel>>> GetUserGroupDetails()
        {
            DataResults<List<UserGroupDetailsModel>> result = new DataResults<List<UserGroupDetailsModel>>();

            try
            {
                var query = await GetUserGroupDetailsBaseQuery().ToListAsync();
                
                result.Result = query;

            }
            catch (Exception ex)
            {

                result.Message = this.configuration["UserGroupDetails:get_courses"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroupDetails:get_courses"], ex.ToString());
            }

            return result;
        }

        public async Task<DataResults<UserGroupDetailsModel>> GetUserGroupDetailsbyUser(string User)
        {
            DataResults<UserGroupDetailsModel> result = new DataResults<UserGroupDetailsModel>();

            try
            {
                UserGroupDetailsModel? userGroupDetails = await GetUserGroupDetailsBaseQuery()
                    .Where(ugd => ugd.UserGroupId.ToString() == User)
                    .FirstOrDefaultAsync();

                if (userGroupDetails != null)
                {
                    result.Result = userGroupDetails;
                }
                else
                {
                    result.Message = this.configuration["UserGroupDetails:user_not_found"];
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserGroupDetails:error_get_user"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroupDetails:error_get_user"], ex.ToString());
            }

            return result;
        }


        public override async Task<bool> Save(UserGroupDetails entity)
        {
            bool result = false;

            try
            {
                if (await base.Exists(ug => ug.Id == entity.Id))
                    throw new Exception(this.configuration["UserGroupDetails:id_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroupDetails:error_save"], ex.ToString());
            }

            return result;
        }

        // Override Update method
        public override async Task<bool> Update(UserGroupDetails entity)
        {
            bool result = false;

            try
            {
                UserGroupDetails? entityToUpdate = this.taxisdb.UserGroupDetails.Find(entity.Id);

                if (entityToUpdate != null)
                {
                    entityToUpdate.UserGroupId = entity.UserGroupId;
                    entityToUpdate.CreationDate = entity.CreationDate;
                    entityToUpdate.ModifyDate = entity.ModifyDate;
                    entityToUpdate.ModifyUser = entity.ModifyUser;

                    result = await base.Update(entityToUpdate);
                }
                else
                {
                    throw new Exception(this.configuration["UserGroupDetails:not_found"]);
                }
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroupDetails:error_update"], ex.ToString());
            }

            return result;
        }

            private IQueryable<UserGroupDetailsModel> GetUserGroupDetailsBaseQuery()
        {
            return from userGroupDetails in this.taxisdb.UserGroupDetails
                   join usergroup in this.taxisdb.UserGroup on userGroupDetails.UserGroupId equals usergroup.Id
                   where userGroupDetails.Deleted == false
                   select new UserGroupDetailsModel()
                   {
                       UserGroupId = usergroup.Id,
                       Id = userGroupDetails.Id, 
                       CreationDate = userGroupDetails.CreationDate,
                        
                   };
        }



    }
}
