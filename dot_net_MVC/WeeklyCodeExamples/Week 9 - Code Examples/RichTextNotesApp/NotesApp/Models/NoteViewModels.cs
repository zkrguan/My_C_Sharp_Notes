using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesApp.Models
{
    public class NoteAddViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Note title")]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        [Display(Name = "Note content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }

    public class NoteBaseViewModel : NoteAddViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(128)]
        [Display(Name = "Note owner")]
        public string Owner { get; set; }

        [Display(Name = "Date created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date of most recent edit")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfLastEdit { get; set; }
    }

    public class NoteEditFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Note title")]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        [Display(Name = "Note content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }

    public class NoteEditViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string Content { get; set; }
    }

}
