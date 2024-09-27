

using System.Linq.Expressions;

namespace TaxisDb.Domain.Interfaces
{
    public interface IRepository<TEntity, TType> where TEntity : class
    {

        //TEntity: Tipo de entidad
        //TData: Tipo de dato que retornan los metodos
        //TType: Tipo de dato el Id

        /*Recibimos una entidad en entity de cualquier tipo y cuando se complete el task
         Devolverá un Tipo de dato en TData, puede ser cualquier tipo de dato, en este 
        caso puede ser la misma entidad guardada, o algún valor adicional */

        Task<bool>  Save(TEntity entity);
        Task<bool>  Update(TEntity entity);
        Task<bool>  Remove(TType Id);
        Task<List<TEntity>>  GetAll();
        Task<TEntity>  GetEntityBy(TType id);

        /*Un filtro de tipo Expression que define una condición (por ejemplo, buscar 
         * usuarios con un nombre específico)*/
        Task<bool>  Exists(Expression<Func<TEntity, bool>> filter);


    }
}
