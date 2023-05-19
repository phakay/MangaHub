namespace MangaHub.Core.Utitlity
{
    public interface IUpdateableAndNotifiable<T> : INotify
    {
        void UpdateAndNotify(T objToUpdate);
    }
}
