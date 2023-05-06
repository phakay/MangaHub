using MangaHub.Controllers;
using MangaHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MangaHub.Core.ViewModels
{
    public class MangaFormViewModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public HttpPostedFileWrapper PictureWrapper { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Action
        { 
            get 
            {
                Expression<Func<MangasController, ActionResult>> update =  c => c.Update(this);
                Expression<Func<MangasController, ActionResult>> create =  c => c.Create(this);
                var action = Id > 0 ? update: create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}