using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetAllGetOne.Data
{
    [Table("Artist")]
    public class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int ArtistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
