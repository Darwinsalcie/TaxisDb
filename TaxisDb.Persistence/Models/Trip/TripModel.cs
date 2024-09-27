using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxisDb.Persistence.Models.Trip
{
    public class TripModel
    {
        public int Id { get; set; }

        public int Taxi_Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int? Calificacion { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
