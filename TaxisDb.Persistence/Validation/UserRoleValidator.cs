using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Validation
{
    public class UserRoleValidator : EntityValidator<UserRole>
    {
        public UserRoleValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(UserRole entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

 

        }
    }
}
