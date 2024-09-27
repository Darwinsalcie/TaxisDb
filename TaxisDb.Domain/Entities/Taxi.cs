
using TaxisDb.Domain.Base;

namespace TaxisDb.Domain.Entities
{
    public class Taxi : AuditEntity<int>
    {
        public override int Id {get; set;}
        public int UserId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public DateTime Año { get; set; }
        public string Modelo { get; set; }
        public int Kilometraje { get; set; }
       // public DateTime CreationDate { get; set; }


    }
}
