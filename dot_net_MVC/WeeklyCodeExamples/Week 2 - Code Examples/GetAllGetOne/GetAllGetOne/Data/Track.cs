using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetAllGetOne.Data
{
    [Table("Track")]
    public class Track
    {
        public Track()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            Playlists = new HashSet<Playlist>();
        }

        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        public Album Album { get; set; }

        public Genre Genre { get; set; }

        public ICollection<InvoiceLine> InvoiceLines { get; set; }

        public MediaType MediaType { get; set; }

        public ICollection<Playlist> Playlists { get; set; }
    }
}
