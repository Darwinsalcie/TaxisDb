using TaxisDb.API.Models.BaseDTO;

namespace TaxisDb.API.Models.Taxi
{
    public record TaxiSaveDTO : BaseSaveEntityDTO
    {
        public int UserId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public DateTime Año { get; set; }
        public string Modelo { get; set; }
        public int Kilometraje { get; set; }

    }
}
