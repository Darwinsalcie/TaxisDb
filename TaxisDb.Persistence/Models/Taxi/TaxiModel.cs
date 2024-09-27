

namespace TaxisDb.Persistence.Models.Taxi
{
    public class TaxiModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public DateTime Año { get; set; }
        public string Modelo { get; set; }
        public int Kilometraje { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
