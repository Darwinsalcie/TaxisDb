

namespace TaxisDb.Persistence.Models.TripDetails
{
    public class TripDetailsModel
    {


        public int TripId { get; set; }
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
