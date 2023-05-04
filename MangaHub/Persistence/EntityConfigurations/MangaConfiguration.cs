using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class MangaConfiguration : EntityTypeConfiguration<Manga>
    {
        public MangaConfiguration()
        {

            Property(m => m.ArtistId)
                .IsRequired();

            Property(m => m.GenreId)
                .IsRequired();

            Property(m => m.Title)
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.Description)
                .IsRequired();

            Property(m => m.Picture)
                .IsRequired();


        }
    }
}