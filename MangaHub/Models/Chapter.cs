using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaHub.Models
{
    public class Chapter
    {
        [Key, Column(Order =0)]
        public int Id { get; set; }
        public Manga Manga { get; set; }
        [Key, Column(Order =1)]
        public int MangaId { get; set; }
        [Key, Column(Order =2)]
        public int Number { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime DateTime { get; set; }
    }
}