using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetAllGetOne.Data
{
    [Table("MediaType")]
    public class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public int MediaTypeId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
