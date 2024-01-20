using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetAllGetOne.Data
{
    [Table("Playlist")]
    public class Playlist
    {
        public Playlist()
        {
            Tracks = new HashSet<Track>();
        }

        public int PlaylistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
