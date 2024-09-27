using Microsoft.Extensions.Configuration;

using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class RoleValidator : EntityValidator<Role>
    {
        public RoleValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(Role entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            // Validación específica para `Rolename`

            ValidateStringField(entity.Description, "Description", 250);

        }
    }

}
