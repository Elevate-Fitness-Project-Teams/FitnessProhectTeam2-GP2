namespace Elevate.Profile.Domain.Interfaces
{
    public interface IGeneralRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public Task<T?> GetById(Guid id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
