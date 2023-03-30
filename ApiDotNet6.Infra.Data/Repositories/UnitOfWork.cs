using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiDotNet6.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        public  async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }
    }
}
