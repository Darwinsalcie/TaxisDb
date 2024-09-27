

using Microsoft.Extensions.Configuration;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class TripValidator : EntityValidator<Trip>
    {
        public TripValidator(IConfiguration configuration) : base(configuration) { }


        public override void Validate(Trip entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            // Validación específica para `Rolename`

            ValidateStringField(entity.Desde, "Desde", 100);
            ValidateStringField(entity.Hasta, "Hasta", 100);

        }
    }
}
