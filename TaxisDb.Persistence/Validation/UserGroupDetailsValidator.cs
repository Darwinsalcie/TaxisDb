using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxisDb.Domain.Base;
using TaxisDb.Domain.Entities;
using TaxisDb.Persistence.Interfaces;

namespace TaxisDb.Persistence.Validation
{
    public class UserGroupDetailsValidator : EntityValidator<UserGroupDetails>
    {

        public UserGroupDetailsValidator(IConfiguration configuration) : base(configuration) { }

        public override void Validate(UserGroupDetails entity)
        {
            // Llamada al método de validación genérica
            base.Validate(entity);

        }

    }
}
