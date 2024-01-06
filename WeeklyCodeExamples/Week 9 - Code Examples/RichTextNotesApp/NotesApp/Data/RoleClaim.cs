using System.ComponentModel.DataAnnotations;

namespace NotesApp.Data
{

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

}