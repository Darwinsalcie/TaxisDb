using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Taxi;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;


namespace TaxisDb.Persistence.Repositories
{
    public class TaxiRepository : BaseRepository<Taxi, int>, ITaxiRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<TaxiRepository> logger;
        private readonly IConfiguration configuration;
        public TaxiRepository(Taxisdb taxisdb,
                               ILogger<TaxiRepository> logger,
                               IConfiguration configuration,
                               TaxiValidator taxiValidator) : base(taxisdb, taxiValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<TaxiModel>>> GetTaxibyUserId(int userId)
        {
            DataResults<List<TaxiModel>> result = new DataResults<List<TaxiModel>>();

            try
            {
                var query = await GetTaxiBaseQuery()
                    .Where(taxi => taxi.UserId == userId)
                    .ToListAsync();
                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["Taxi:get_taxi_user_id"];
                result.Success = false;
                this.logger.LogError(this.configuration["Taxi:get_taxi_user_id"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<List<TaxiModel>>> GetTaxis()
        {
            DataResults<List<TaxiModel>> result = new DataResults<List<TaxiModel>>();

            try
            {
                var query = await GetTaxiBaseQuery().ToListAsync();
                result.Result = query;

            }
            catch (Exception ex)
            {

                result.Message = this.configuration["taxi:get_courses"];
                result.Success = false;
                this.logger.LogError(this.configuration["taxi:get_courses"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<TaxiModel>> GetTaxibyPlaca(string placa)
        {
            DataResults<TaxiModel> result = new DataResults<TaxiModel>();
            try
            {
                TaxiModel? dato = await GetTaxiBaseQuery()
                                        .Where(taxi => taxi.Placa == placa)
                                         .FirstOrDefaultAsync();


                result.Result = dato;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["Taxi:get_course_name"];
                result.Success = false;
                this.logger.LogError(this.configuration["Taxi:get_course_name"], ex.ToString());

            }

            return result;
        }

        public override async Task<bool> Save(Taxi entity)
        {
            bool result = false;

            try
            {
                if (await base.Exists(taxi => taxi.Id == entity.Id))
                    throw new EntityDataException(this.configuration["Taxi:id_exists"]);
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["tAXI:error_save"], ex.ToString());
            }
            return result;

        }

        public override async Task<bool> Update(Taxi entity)
        {
            bool result = false;
            try
            {

                //if (!entity.CreationDate.HasValue)
                //    throw new RoleDataException(this.configuration["Role:start_date_is_null"]);


                Taxi? taxiToUpdate = this.taxisdb.Taxi.Find(entity.Id);

                taxiToUpdate.UserId = entity.UserId;
                taxiToUpdate.Placa = entity.Placa;
                taxiToUpdate.Modelo = entity.Modelo;
                taxiToUpdate.Kilometraje = entity.Kilometraje;
                taxiToUpdate.CreationDate = entity.CreationDate;
                taxiToUpdate.ModifyDate = entity.ModifyDate;
                taxiToUpdate.ModifyUser = entity.ModifyUser;
                taxiToUpdate.Año = entity.Año;
                taxiToUpdate.Marca = entity.Marca;

                result = await base.Update(taxiToUpdate);

            }
            catch (Exception ex)
            {

                result = false;
                this.logger.LogError(this.configuration["taxi:error_update"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Remove(int id)
        {
            bool result = false;
            try
            {
                Taxi? taxiToRemove = await this.taxisdb.Taxi.FindAsync(id);
                if (taxiToRemove != null)
                {
                    taxiToRemove.Deleted = true; // Marcar como eliminado
                    result = await base.Update(taxiToRemove);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(this.configuration["Taxi:error_remove"], ex.ToString());
            }
            return result;
        }

        private IQueryable<TaxiModel> GetTaxiBaseQuery()
        {
            return from taxi in this.taxisdb.Taxi
                   join user in this.taxisdb.User on taxi.UserId equals user.Id
                   where taxi.Deleted  != true
                   select new TaxiModel()
                   {   Id = taxi.Id,
                       UserId = user.Id,
                       CreationDate = taxi.CreationDate,
                       Placa = taxi.Placa,
                       Marca = taxi.Marca,
                       Año = taxi.Año,
                       Modelo = taxi.Modelo,
                       Kilometraje = taxi.Kilometraje,
                   };
        }


    }
}
