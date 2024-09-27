
using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class TripDetails : AuditEntity<int>
    {
        //public override TripDetailsKey Id { get; set; }
        
        public override int Id {  get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
