using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MangaHub.Tests.Extensions
{
    public static class MockDbSetExtensions
    {
        public static void SetSource<T>(this Mock<DbSet<T>> mockDbSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();
            mockDbSet.As<IQueryable<T>>().SetupGet(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().SetupGet(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().SetupGet(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
