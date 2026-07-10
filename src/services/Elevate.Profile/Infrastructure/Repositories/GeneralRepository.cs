using Elevate.Profile.Domain.Interfaces;

namespace Elevate.Profile.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        private readonly ProfileDbContext _context;

        public GeneralRepository(ProfileDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

       

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
