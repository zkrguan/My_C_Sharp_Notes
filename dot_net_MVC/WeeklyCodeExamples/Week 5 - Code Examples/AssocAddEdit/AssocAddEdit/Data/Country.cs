using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssocAddEdit.Data
{
    public class Country
    {
        public Country()
        {
            Manufacturers = new List<Manufacturer>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}