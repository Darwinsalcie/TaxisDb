
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Trip;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;

namespace TaxisDb.Persistence.Repositories
{
    public class TripRepository : BaseRepository<Trip, int>, ITripRepository
    {
        private readonly Taxisdb taxisdb;
        private readonly ILogger<TripRepository> logger;
        private readonly IConfiguration configuration;

        public TripRepository(Taxisdb taxisdb,
                               ILogger<TripRepository> logger,
                               IConfiguration configuration,
                               TripValidator tripValidator) : base(taxisdb, tripValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }
        
        
        public async Task<DataResults<List<TripModel>>> GetTrip()
        {
            DataResults<List<TripModel>> result = new DataResults<List<TripModel>>();

            try
            {
                var query = await GetTripBaseQuery().ToListAsync();
                result.Result = query;

            }
            catch (Exception ex)
            {

                result.Message = this.configuration["trip:get_courses"];
                result.Success = false;
                this.logger.LogError(this.configuration["trip:get_courses"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<TripModel>> GetTripbyId(int id)
        {
            DataResults<TripModel> result = new DataResults<TripModel>();
            try
            {
                TripModel? dato = await GetTripBaseQuery()
                                        .Where(trip => trip.Id == id)
                                         .FirstOrDefaultAsync();


                result.Result = dato;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["Trip:get_course_name"];
                result.Success = false;
                this.logger.LogError(this.configuration["Trip:get_course_name"], ex.ToString());

            }

            return result;
        }

        public async Task<DataResults<List<TripModel>>> GetTripsbyTaxiId(int TaxiId)
        {
            DataResults<List<TripModel>> result = new DataResults<List<TripModel>>();

            try
            {
                var query = await GetTripBaseQuery()
                    .Where(trip => trip.Taxi_Id == TaxiId)
                    .ToListAsync();


            }

            catch (Exception ex)
            {
                {
                    result.Message = this.configuration["Taxi:get_taxi_user_id"];
                    result.Success = false;
                    this.logger.LogError(this.configuration["Taxi:get_taxi_user_id"], ex.ToString());

                }


            }

            return result;

        }


        public override async Task<bool> Save(Trip entity)
        {
            bool result = false;

            try
            {
                // El validador ya se maneja en el BaseRepository
                if (await base.Exists(trip => trip.Id == entity.Id))
                    throw new EntityDataException(this.configuration["Trip:name_exists"]);

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["Trip:error_save"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Update(Trip entity)
        {

            bool result = false;
            try
            {

                //if (!entity.CreationDate.HasValue)
                //    throw new RoleDataException(this.configuration["Role:start_date_is_null"]);


                Trip? tripToUpdate = this.taxisdb.Trip.Find(entity.Id);

                tripToUpdate.Taxi_Id = entity.Taxi_Id;
                tripToUpdate.Desde = entity.Desde;
                tripToUpdate.Hasta = entity.Hasta;
                tripToUpdate.Calificacion = entity.Calificacion;
                tripToUpdate.ModifyDate = entity.ModifyDate;
                tripToUpdate.ModifyUser = entity.ModifyUser;


                result = await base.Update(tripToUpdate);

            }
            catch (Exception ex)
            {

                result = false;
                this.logger.LogError(this.configuration["trip:error_update"], ex.ToString());
            }
            return result;
        }
  

        private IQueryable<TripModel> GetTripBaseQuery()
        {
            return from trip in this.taxisdb.Trip
                   //join taxi in this.taxisdb.Trip on trip.Taxi_Id equals taxi.Id
                   where trip.Deleted != true

                   select new TripModel()
                   {
                       Id = trip.Id,
                       Taxi_Id = trip.Id,
                       FechaInicio = trip.FechaInicio,
                       FechaFin = trip.FechaFin,
                       Desde = trip.Desde,
                       Hasta = trip.Hasta,
                       Calificacion = trip.Calificacion,
                       CreationDate = trip.CreationDate,


                   };
        }


        public override async Task<bool> Remove(int id)
        {
            bool result = false;
            try
            {
                Trip? tripToRemove= await this.taxisdb.Trip.FindAsync(id);
                if (tripToRemove != null)
                {
                    tripToRemove.Deleted = true; // Marcar como eliminado
                    result = await base.Update(tripToRemove);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(this.configuration["Trip:error_remove"], ex.ToString());
            }
            return result;
        }



    }
}
