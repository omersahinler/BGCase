using StockAPI.Domain.Core.Base.Abstract;
using StockAPI.Domain.Core.Pagination;
using System.Linq.Expressions;

namespace StockAPI.Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity> GetByIdAsync(Guid id);

    (IQueryable<TEntity>, int) GetAll(PaginationParams paginationParams);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    void Remove(TEntity entity);

    void Update(TEntity entity);

    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

}