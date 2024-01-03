using System.ComponentModel.DataAnnotations.Schema;

namespace GetAllGetOne.Data
{
    [Table("InvoiceLine")]
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        public int TrackId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Invoice Invoice { get; set; }

        public Track Track { get; set; }
    }
}
