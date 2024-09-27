using TaxisDb.Web.Models.BaseDTO;

namespace TaxisDb.Web.Models.Taxi
{
    public record TaxiUpdateDTO : BaseUpdateEntityDTO
    {
        public int UserId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public DateTime Año { get; set; }
        public string Modelo { get; set; }
        public int Kilometraje { get; set; }
    }
}
