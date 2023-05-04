using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class ChapterConfiguration : EntityTypeConfiguration<Chapter>
    {
        public ChapterConfiguration()
        {
            Property(c => c.ChapterNo)
                .HasColumnOrder(0);

            Property(c => c.MangaId)
                .HasColumnOrder(1);
             
            HasKey(c => new { c.MangaId, c.ChapterNo });
        }
    }
}