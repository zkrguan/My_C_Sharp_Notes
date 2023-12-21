using System.ComponentModel.DataAnnotations;

namespace AssocAddEdit.Data
{
    public class Classification
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

    }
}