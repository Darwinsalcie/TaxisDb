

using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class Trip : AuditEntity<int>
    {
        public override int Id { get; set; }

        public int Taxi_Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int? Calificacion { get; set; }

    }
}
