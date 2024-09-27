
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaxisDb.Domain.Interfaces;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Validation;


namespace TaxisDb.Persistence.Repository
{
    public abstract class BaseRepository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : class
    {
       
        
        private readonly Taxisdb _dbContext;
        private DbSet<TEntity> _dbSet;
        private readonly EntityValidator<TEntity> validator;
        public BaseRepository(Taxisdb dbContext, EntityValidator<TEntity> validator)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            this.validator = validator;
        }


        public virtual async Task<bool>  Exists(Expression<Func<TEntity, bool>> filter)
        {
            bool result = false;
            try
            {
                result = await _dbSet.AnyAsync(filter);
            }
            catch (Exception ex) {

                result = false;
            }
            return result;
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity>  GetEntityBy(TType Id)
        {
            return await this._dbSet.FindAsync(Id);
        }

        public virtual async Task<bool> Remove(TType id)
        {
            TEntity? entityToRemove = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entityToRemove == null)
            {
                return false;
            }

            _dbContext.Set<TEntity>().Remove(entityToRemove);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Save(TEntity entity)
        {


            bool result = false;
            try
            {
                // Validación antes de guardar
                validator.Validate(entity);
                _dbSet.Add(entity);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public virtual async Task<bool> Update(TEntity entity)
        {
                       bool result = false;
            try
            {
                // Validación antes de actualizar
                validator.Validate(entity);
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
