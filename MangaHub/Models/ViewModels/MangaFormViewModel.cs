﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MangaHub.Models.ViewModels
{
    public class MangaFormViewModel
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public HttpPostedFileWrapper Picture { get; set; }
        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}