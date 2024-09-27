using TaxisDb.Web.Models.BaseDTO;

namespace TaxisDb.Web.Models.Trip
{
    public record TripSaveDTO : BaseSaveEntityDTO
    {
        public int Taxi_Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int? Calificacion { get; set; }
    }
}
