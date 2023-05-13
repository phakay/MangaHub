using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class ReadingConfiguration : EntityTypeConfiguration<Reading>
    {
        public ReadingConfiguration()
        {
   
            HasKey(r => new { r.MangaId, r.UserId });

            HasRequired(r => r.Manga)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}