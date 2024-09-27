using Microsoft.Extensions.Configuration;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class UserGroupRequestsValidator : EntityValidator<UserGroupRequests>
    {
        public UserGroupRequestsValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(UserGroupRequests entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            // Validación específica el campo 

            ValidateStringField(entity.Status, "Status", 20);

        }
    }
}
