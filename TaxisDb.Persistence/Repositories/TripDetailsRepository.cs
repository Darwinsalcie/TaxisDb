using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Exceptions;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.TripDetails;
using TaxisDb.Persistence.Repository;
using TaxisDb.Persistence.Validation;


namespace TaxisDb.Persistence.Repositories
{
    public class TripDetailsRepository : BaseRepository<TripDetails, int>, ITripDetailsRepository
    {

        private readonly Taxisdb taxisdb;
        private readonly ILogger<TripDetailsRepository> logger;
        private readonly IConfiguration configuration;

        public TripDetailsRepository(Taxisdb taxisdb,
                               ILogger<TripDetailsRepository> logger,
                               IConfiguration configuration,
                               TripDetailsValidator tripDetailsValidator) : base(taxisdb, tripDetailsValidator)
        {

            this.taxisdb = taxisdb;
            this.logger = logger;
            this.configuration = configuration;


        }

        public async Task<DataResults<List<TripDetailsModel>>> GetTripDetails()
        {
            DataResults<List<TripDetailsModel>> result = new DataResults<List<TripDetailsModel>>();

            try
            {
                var query = await GetTripDetailsBaseQuery()
                                        
                                        .ToListAsync();




                result.Result = query;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["tripDetails:get_tripDetails"];
                result.Success = false;
                this.logger.LogError(this.configuration["tripDetails:get_tripDetails"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResults<List<TripDetailsModel>>> GetTripDetailsbyTrip(int TripId)
        {
            DataResults<List<TripDetailsModel>> result = new DataResults<List<TripDetailsModel>>();

            try
            {
                var query = await GetTripDetailsBaseQuery()
                    .Where(taxi => taxi.TripId == TripId)
                    .ToListAsync();

                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["TripDetails:get_taxi_user_id"];
                result.Success = false;
                this.logger.LogError(this.configuration["TripDetails:get_taxi_user_id"], ex.ToString());
            }
            return result;
        }


        public override async Task<bool> Save(TripDetails entity)
        {
            bool result = false;

            try
            {
                if (await base.Exists(tdet => tdet.TripId == entity.TripId && tdet.UserId == entity.UserId))
                    throw new EntityDataException(this.configuration["tdet:id_exists"]);
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["TripDetails:error_save"], ex.ToString());
            }
            return result;
        }

        public override async Task<bool> Update(TripDetails entity)
        {
            bool result = false;
            try
            {
                TripDetails? triDetailsToUpdate = await this.taxisdb.TripDetails
                    .FirstOrDefaultAsync(t => t.TripId == entity.TripId && t.UserId == entity.UserId);

                if (triDetailsToUpdate != null)
                {
                    triDetailsToUpdate.Fecha = entity.Fecha;
                    triDetailsToUpdate.Latitude = entity.Latitude;
                    triDetailsToUpdate.Longitude = entity.Longitude;
                    triDetailsToUpdate.ModifyUser = entity.ModifyUser;

                    result = await base.Update(triDetailsToUpdate);
                }
                else
                {
                    throw new EntityDataException(this.configuration["TripDetails:not_found"]);
                }
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["TripDetails:error_update"], ex.ToString());
            }
            return result;
        }


        private IQueryable<TripDetailsModel> GetTripDetailsBaseQuery()
        {
            return from tripDetails in this.taxisdb.TripDetails
                   join trip in this.taxisdb.Trip on tripDetails.TripId equals trip.Id
                   join user in this.taxisdb.Trip on tripDetails.UserId equals user.Id
                   where tripDetails.Deleted == false

                   select new TripDetailsModel()
                   {

                       TripId = trip.Id,
                       UserId = user.Id,
                       Fecha = tripDetails.Fecha,
                       Latitude = tripDetails.Latitude,
                       Longitude = tripDetails.Longitude,
                       CreationDate = tripDetails.CreationDate



                   };
        }

    }
}
