using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomStoreForCarBusiness.Data
{
    // TODO 02 - Car business data model, Country design model class
    public class Country
    {
        public Country()
        {
            Manufacturers = new HashSet<Manufacturer>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}