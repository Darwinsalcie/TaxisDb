using Microsoft.Extensions.Configuration;

using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class TaxiValidator : EntityValidator<Taxi>
    {
        public TaxiValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(Taxi entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            // Validación específica para `Taxiname`
            ValidateStringField(entity.Placa, "Taxi:name", 50);
        }
    }
}
