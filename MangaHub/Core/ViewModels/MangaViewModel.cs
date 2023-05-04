using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.ViewModels
{
    public class MangaViewModel
    {
        public IEnumerable<Manga> Mangas { get; set; }
    }
}