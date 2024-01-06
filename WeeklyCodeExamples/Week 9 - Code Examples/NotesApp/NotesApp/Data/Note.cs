using System;
using System.ComponentModel.DataAnnotations;

namespace NotesApp.Data
{

    public class Note
    {
        public Note()
        {
            DateCreated = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string Content { get; set; }

        [Required, StringLength(128)]
        public string Owner { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateOfLastEdit { get; set; }
    }

}