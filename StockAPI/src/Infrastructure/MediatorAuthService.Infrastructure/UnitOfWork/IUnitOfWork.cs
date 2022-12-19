using StockAPI.Domain.Core.Base.Abstract;
using StockAPI.Domain.Core.Base.Concrete;
using StockAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace StockAPI.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, IEntity;

    Task<int> SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitAsync(bool isSaveChanges = true);

    Task RollBackAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork, IAsyncDisposable where TContext : DbContext
{
    TContext Context { get; }
}