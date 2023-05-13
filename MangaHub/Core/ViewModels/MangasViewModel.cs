using MangaHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Core.ViewModels
{
    public class MangasViewModel
    {
        public IEnumerable<Manga> Mangas { get; set; }
        public bool ShowInfo { get; set; }
        public ILookup<int,Reading> UserReadings { get; set; }

        public MangasViewModel()
        {
            UserReadings = Enumerable.Empty<Reading>()
                                    .ToLookup(r => r.MangaId);
        }
    }
}