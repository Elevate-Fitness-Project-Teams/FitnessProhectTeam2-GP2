using Elevate.Profile.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elevate.Profile.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ProfileDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        private int _depth;

        public UnitOfWork(ProfileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            bool isOutermost = _transaction == null;
            if (isOutermost)
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync();
            }

            _depth++;

            try
            {
                await action();
                await _dbContext.SaveChangesAsync();
                if (isOutermost)
                {
                    await _transaction!.CommitAsync();
                }
            }
            catch
            {
               
                if (isOutermost)
                {
                    await _transaction!.RollbackAsync();
                }
                throw;
            }
            finally
            {
                _depth--;
                if (isOutermost)
                {
                    await _transaction!.DisposeAsync();
                    _transaction = null;
                }
            }
        }
    }
}