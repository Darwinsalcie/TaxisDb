using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.UserGroupRequests;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class UserGroupRequestsRepository : BaseRepository<UserGroupRequests, int>, IUserGroupRequestsRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<UserGroupRequests> logger;
        private readonly IConfiguration configuration;
        public UserGroupRequestsRepository(Taxisdb taxisdb,
                               ILogger<UserGroupRequests> logger,
                               IConfiguration configuration,
                               UserGroupRequestsValidator userGroupRequestsValidator) : base(taxisdb, userGroupRequestsValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<UserGroupRequestsModel>>> GetUserGroupRequests()
        {
            DataResults<List<UserGroupRequestsModel>> result = new DataResults<List<UserGroupRequestsModel>>();
            try
            {
                var query = await GetUserGroupRequestsBaseQuery().ToListAsync();
                result.Result = query;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserGroupRequests:get_requests_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroupRequests:get_requests_error"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<UserGroupRequestsModel>> GetUserGroupRequestsbyName(string UGRName)
        {
            DataResults<UserGroupRequestsModel> result = new DataResults<UserGroupRequestsModel>();
            try
            {
                var request = await GetUserGroupRequestsBaseQuery()
                                    .Where(req => req.Status == UGRName)
                                    .FirstOrDefaultAsync();
                result.Result = request;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserGroupRequests:get_request_by_name_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserGroupRequests:get_request_by_name_error"], ex.ToString());
            }
            return result;
        }


        public override async Task<bool> Save(UserGroupRequests entity)
        {
            bool result = false;
            try
            {
                if (await base.Exists(req => req.Id == entity.Id))
                    throw new EntityDataException(this.configuration["UserGroupRequests:id_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroupRequests:error_save"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Update(UserGroupRequests entity)
        {
            bool result = false;
            try
            {
                UserGroupRequests? requestToUpdate = this.taxisdb.UserGroupRequests.Find(entity.Id);

                if (requestToUpdate == null)
                    throw new EntityDataException(this.configuration["UserGroupRequests:not_found"]);

                requestToUpdate.UserGroupId = entity.UserGroupId;
                requestToUpdate.RequestDate = entity.RequestDate;
                requestToUpdate.Status = entity.Status;
                requestToUpdate.ModifyDate = entity.ModifyDate;
                requestToUpdate.ModifyUser = entity.ModifyUser;

                result = await base.Update(requestToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserGroupRequests:error_update"], ex.ToString());
            }
            return result;
        }

        private IQueryable<UserGroupRequestsModel> GetUserGroupRequestsBaseQuery()
        {
            return from request in this.taxisdb.UserGroupRequests
                   where request.Deleted == false
                   select new UserGroupRequestsModel
                   {
                       Id = request.Id,
                       UserGroupId = request.UserGroupId,
                       RequestDate = request.RequestDate,
                       Status = request.Status
                   };
        }
    }
}
