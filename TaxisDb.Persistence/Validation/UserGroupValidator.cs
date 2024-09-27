using Microsoft.Extensions.Configuration;
using TaxisDb.Domain.Entities;
using TaxisDb.Persistence.Exceptions;

namespace TaxisDb.Persistence.Validation
{
    public class UserGroupValidator : EntityValidator<UserGroup> 
    {
        public UserGroupValidator(IConfiguration configuration) : base(configuration) { }

        public override async void Validate(UserGroup entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);


            // Validación específica para los campos
            ValidateStringField(entity.GroupName, "GroupName", 50);

            ValidateStringField(entity.Description, "Description", 255);

        }
    }
}
