
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Models.Taxi;

namespace TaxisDb.Persistence.Interfaces
{
    public interface ITaxiRepository : IRepository<Taxi, int>
    {
        /// <summary>
        /// Get All UsersList Method
        /// </summary>
        /// <returns></returns>
        Task<DataResults<List<TaxiModel>>> GetTaxis();

        /// <summary>
        /// Get user by name Method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DataResults<List<TaxiModel>>> GetTaxibyUserId(int userId);
        /// <summary>
        /// Get Taxi by PLaca
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        Task<DataResults<TaxiModel>> GetTaxibyPlaca(string placa);
        /// <summary>
        /// Get Taxi by taxiId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResults<List<TaxiModel>>> GetTaxibyId(int Id);

    }
}
