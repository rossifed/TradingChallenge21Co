using Microsoft.EntityFrameworkCore;
namespace QuantLab.Shared.Infrastructure.Database
{
    public abstract class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private T DbContext { get; }

        protected UnitOfWork(T dbContext)
        {
            this.DbContext = dbContext;
        }
        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Result> ExecuteAsync<Result>(Func<Task<Result>> action)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                Result result = await action();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
