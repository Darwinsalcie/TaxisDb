
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;

using TaxisDb.Persistence.Models.Trip;

namespace TaxisDb.Persistence.Interfaces
{
    public interface ITripRepository : IRepository<Trip, int>
    {
        /// <summary>
        /// Get All TripList Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<TripModel>>> GetTrip();

        /// <summary>
        /// Get Tip by tripId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResults<TripModel>> GetTripbyId(int Id);

        /// <summary>
        /// /Get trip by taxiId
        /// </summary>
        /// <param name="TaxiId"></param>
        /// <returns></returns>
        Task<DataResults<List<TripModel>>> GetTripsbyTaxiId(int TaxiId);
   
    }
}
