using Microsoft.Extensions.Configuration;
using System.Data;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class TripDetailsValidator : EntityValidator<TripDetails>
    {
        public TripDetailsValidator(IConfiguration configuration) : base(configuration) { }
        public override void Validate(TripDetails entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            

        }     
        
    }
}
