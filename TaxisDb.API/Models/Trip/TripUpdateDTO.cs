using TaxisDb.API.Models.BaseDTO;

namespace TaxisDb.API.Models.Trip
{
    public record TripUpdateDTO : BaseUpdateEntityDTO
    {
        public int Taxi_Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int? Calificacion { get; set; }
    }
}