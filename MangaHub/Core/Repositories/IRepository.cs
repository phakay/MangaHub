namespace MangaHub.Core.Repositories
{
    public interface IRepository<T> where T: class
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
