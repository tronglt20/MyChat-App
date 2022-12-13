namespace Domain.Interfaces.Base
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : class;

        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);

        Task<int> SaveChangesAsync();
    }
}
