namespace MangaHub.Utility
{
    interface INotifiableOnUpdate<T> where T : class
    {
        string GetNotificationMessageForUpdate(T objWithUpdate);
    }
}
