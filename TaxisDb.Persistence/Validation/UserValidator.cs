using Microsoft.Extensions.Configuration;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class UserValidator : EntityValidator<User>
    {
        public UserValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(User entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

            // Validación específica para los campos

            ValidateStringField(entity.Documento, "Documento", 20);
            ValidateStringField(entity.Nombre, "Nombre", 50);
            ValidateStringField(entity.Apellido, "Apellido", 50);

        }
    }
}
