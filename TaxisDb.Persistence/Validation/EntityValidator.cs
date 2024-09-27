using Microsoft.Extensions.Configuration;
using System.Data;


namespace TaxisDb.Persistence.Validation
{
    public abstract class EntityValidator<T> : Exception
    {
        protected readonly IConfiguration configuration;

        protected EntityValidator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        // Validación genérica para verificar si la entidad es nula
        public virtual void Validate(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(this.configuration["Validation:EntityNull"]);
        }

        // Validación genérica para cadenas de texto
        protected void ValidateStringField(string field, string fieldName, int maxLength)
        {
            if (string.IsNullOrEmpty(field))
                throw new DataException(this.configuration[$"Validation:{fieldName}_is_null"]);

            if (field.Length > maxLength)
                throw new DataException(this.configuration[$"Validation:{fieldName}_length"].Replace("{fieldName}", fieldName));
        }

    }

}
