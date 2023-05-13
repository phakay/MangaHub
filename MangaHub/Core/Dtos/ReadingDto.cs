using System.ComponentModel.DataAnnotations;

namespace MangaHub.Core.Dtos
{
    public class ReadingDto
    {
        [Required, Range(1, int.MaxValue)]
        public int MangaId { get; set; }
    }
}