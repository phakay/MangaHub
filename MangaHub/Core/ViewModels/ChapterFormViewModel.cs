using System.ComponentModel.DataAnnotations;

namespace MangaHub.Core.ViewModels
{
    public class ChapterFormViewModel
    {
        [Required]
        public int MangaId { get; set; }

        public byte[] MangaPicture { get; set; }
        public string MangaTitle  { get; set; }


        [Range(1, int.MaxValue)]
        public int ChapterNo { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }

        [Required]
        public string Information { get; set; }
    }
}

