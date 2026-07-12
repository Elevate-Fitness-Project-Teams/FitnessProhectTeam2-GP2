namespace Elevate.Profile.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
