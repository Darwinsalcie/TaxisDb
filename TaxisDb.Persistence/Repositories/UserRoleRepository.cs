using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.UserRole;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole, int>, IUserRoleRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<UserRoleRepository> logger;
        private readonly IConfiguration configuration;
        public UserRoleRepository(Taxisdb taxisdb,
                               ILogger<UserRoleRepository> logger,
                               IConfiguration configuration,
                               UserRoleValidator userRoleValidator) : base(taxisdb, userRoleValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<UserRoleModel>>> GetUserRole()
        {
            DataResults<List<UserRoleModel>> result = new DataResults<List<UserRoleModel>>();
            try
            {
                var roles = await GetUserRoleBaseQuery().ToListAsync();
                result.Result = roles;
                result.Success = true; // Asume éxito si se obtienen roles
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserRole:get_all_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserRole:get_all_error"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<List<UserRoleModel>>> GetUserRolebyUserId(int userId)
        {
            DataResults<List<UserRoleModel>> result = new DataResults<List<UserRoleModel>>();
            try
            {
                var roleByUser = await GetUserRoleBaseQuery()
                                .Where(rol => rol.UserId == userId)
                                .ToListAsync();

                result.Result = roleByUser;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserRole:get_by_name_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserRole:get_by_name_error"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<List<UserRoleModel>>> GetUserRolebyRoleId(int roleId)
        {
            DataResults<List<UserRoleModel>> result = new DataResults<List<UserRoleModel>>();
            try
            {
                var usersByRole = await GetUserRoleBaseQuery()
                                         .Where(userRole => userRole.RoleId == roleId)
                                         .ToListAsync();

                result.Result = usersByRole;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["UserRole:get_by_role_id_error"];
                result.Success = false;
                this.logger.LogError(this.configuration["UserRole:get_by_role_id_error"], ex.ToString());
            }

            return result;
        }

        public override async Task<bool> Save(UserRole entity)
        {
            bool result = false;

            try
            {
                // Verifica si el UserRole ya existe
                if (await base.Exists(userRole => userRole.UserId == entity.UserId && userRole.RoleId == entity.RoleId))
                    throw new EntityDataException(this.configuration["UserRole:exists_error"]);

                // Llama al método base para guardar la entidad
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserRole:error_save"], ex.ToString());
            }

            return result;
        }

        public override async Task<bool> Update(UserRole entity)
        {
            bool result = false;

            try
            {
                // Busca la entidad existente en la base de datos
                UserRole? userRoleToUpdate = await this.taxisdb.UserRole.FindAsync(entity.UserId, entity.RoleId);

                if (userRoleToUpdate == null)
                {
                    throw new EntityDataException(this.configuration["UserRole:not_found_error"]);
                }

                // Actualiza las propiedades necesarias
                userRoleToUpdate.RoleId = entity.RoleId;

                // Llama al método base para actualizar la entidad
                result = await base.Update(userRoleToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["UserRole:error_update"], ex.ToString());
            }

            return result;
        }
        private IQueryable<UserRoleModel> GetUserRoleBaseQuery()
        {
            return from userRole in this.taxisdb.UserRole
                   join user in this.taxisdb.User on userRole.UserId equals user.Id
                   join role in this.taxisdb.Role on userRole.RoleId equals role.Id
                   where userRole.Deleted == false // Asegúrate de filtrar roles eliminados
                   select new UserRoleModel()
                   {
                       UserId = user.Id,
                       RoleId = role.Id,
                       UserName = user.Nombre, // Suponiendo que 'Nombre' es la propiedad para el nombre de usuario
                       RoleName = role.Rolename,
                       // Agrega otras propiedades si es necesario, por ejemplo:
                       // UserName = user.Name,
                       // RoleName = role.Name
                   };
        }


    }
}

