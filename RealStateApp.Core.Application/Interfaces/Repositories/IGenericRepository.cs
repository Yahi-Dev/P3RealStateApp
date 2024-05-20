using System.Linq.Expressions;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, int Id);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetEntityByIdAsync(int Id);
        Task<List<TEntity>> GetAllWithIncludeAsync(List<string> properties);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
