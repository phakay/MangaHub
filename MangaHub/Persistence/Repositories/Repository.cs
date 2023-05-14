using MangaHub.Core.Repositories;
using System.Data.Entity;

namespace MangaHub.Persistence.Repositories
{
    public class Repository<T>  : IRepository<T> where T: class
    {
        private readonly IDbSet<T> _dbSet;
        public Repository(IDbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}