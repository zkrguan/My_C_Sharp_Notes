using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LINQIntroduction.Data
{
    [Table("Genre")]
    public class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public int GenreId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
