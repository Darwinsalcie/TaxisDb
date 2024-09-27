
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Role;
using TaxisDb.Persistence.Repository;
using System.Data;
using TaxisDb.Persistence.Validation;
using TaxisDb.Persistence.Exceptions;

namespace TaxisDb.Persistence.Repositories
{
    public sealed class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<RoleRepository> logger;
        private readonly IConfiguration configuration;

        public RoleRepository(Taxisdb taxisdb,
                               ILogger<RoleRepository> logger,
                               IConfiguration configuration,
                               RoleValidator roleValidator) : base(taxisdb, roleValidator)
        {
            
            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        
        }

        public async Task<DataResults<List<RoleModel>>> GetRoles()
        {
            DataResults<List<RoleModel>> result = new DataResults<List<RoleModel>>();
            try
            {
                var roles = await (from rol in this.taxisdb.Role
                                        where rol.Deleted != true
                                        orderby rol.CreationDate descending
                                        select new RoleModel
                                        {
                                            Id = rol.Id,
                                            Rolename = rol.Rolename,
                                            Description = rol.Description,
                                            CreationDate = rol.CreationDate,
                                            CreationUser = rol.CreationUser

                                        }).ToListAsync();

                result.Result = roles;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = this.configuration["Role:error_get_roles"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<RoleModel>> GetRoles(string rolename)
        {
            DataResults<RoleModel> result = new DataResults<RoleModel>();
            try
            {
                var role = await this.taxisdb.Role
                                                       .SingleOrDefaultAsync(rol => rol.Rolename == rolename
                                                                             && rol.Deleted == false);

                if (role is null)
                {
                    result.Message = this.configuration["Role:error_get_roles"];
                    result.Success = false;
                    return result;
                }

                result.Result = new RoleModel()
                {
                    Id = role.Id,
                    Description = role.Description,
                    Rolename = role.Rolename,
                    CreationUser = role.CreationUser,
                    CreationDate = role.CreationDate.Date,
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["Role:error_get_role_name"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Save(Role entity)
        {
            bool result = false;

            try
            {
                // El validador ya se maneja en el BaseRepository
                if (await base.Exists(rol => rol.Rolename == entity.Rolename))
                    throw new EntityDataException(this.configuration["Role:name_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["Role:error_save"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Remove(int id)
        {
            bool result = false;
            try
            {
                Role? roleToRemove = await this.taxisdb.Role.FindAsync(id);
                if (roleToRemove != null)
                {
                    roleToRemove.Deleted = true; // Marcar como eliminado
                    result = await base.Update(roleToRemove);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(this.configuration["Role:error_remove"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Update(Role entity)
        {
            bool result = false;
            try
            {

                //if (!entity.CreationDate.HasValue)
                //    throw new RoleDataException(this.configuration["Role:start_date_is_null"]);


                Role? roleToUpdate = this.taxisdb.Role.Find(entity.Id);

                roleToUpdate.Rolename= entity.Rolename;
                roleToUpdate.CreationDate = entity.CreationDate;
                roleToUpdate.ModifyUser = entity.ModifyUser;
                roleToUpdate.ModifyDate = entity.ModifyDate;
                roleToUpdate.Description= entity.Description;
                roleToUpdate.CreationUser = entity.CreationUser;

                result = await base.Update(roleToUpdate);

            }
            catch (Exception ex)
            {

                result = false;
                this.logger.LogError(this.configuration["role:error_update"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<RoleModel>> GetRolesById(int Id)
        {
            DataResults<RoleModel> result = new DataResults<RoleModel>();
            try
            {
                var role = await this.taxisdb.Role
                                                       .SingleOrDefaultAsync(rol => rol.Id == Id
                                                                             && rol.Deleted != true);

                if (role is null)
                {
                    result.Message = this.configuration["Role:error_get_roles"];
                    result.Success = false;
                    return result;
                }

                result.Result = new RoleModel()
                {
                    Id = role.Id,
                    Description = role.Description,
                    Rolename = role.Rolename,
                    CreationUser = role.CreationUser,
                    CreationDate = role.CreationDate.Date,
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["Role:error_get_role_name"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
