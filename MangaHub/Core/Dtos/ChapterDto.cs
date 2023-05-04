using System.ComponentModel.DataAnnotations;

namespace MangaHub.Core.Dtos
{
    public class ChapterDto
    {
        [Required]
        public int MangaId { get; set; }
        [Range(1, int.MaxValue)]
        public int ChapterNo { get; set; }
        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }
        [Required]
        public string Information { get; set; }
    }
}