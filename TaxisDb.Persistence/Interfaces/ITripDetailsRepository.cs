

using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.TripDetails;

namespace TaxisDb.Persistence.Interfaces
{
    public interface ITripDetailsRepository : IRepository<TripDetails, int>
    {
        /// <summary>
        /// Get All tripdetails Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<TripDetailsModel>>> GetTripDetails();

        /// <summary>
        /// Get tripdetails by name Method
        /// </summary>
        /// <param name="TripId"></param>
        /// <returns></returns>
        Task<DataResults<List<TripDetailsModel>>> GetTripDetailsbyTrip(int TripId);

    }
}
