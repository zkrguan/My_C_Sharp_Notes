using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomStoreForCarBusiness.Data
{
    // TODO 03 - Manufacturer design model class
    public class Manufacturer
    {
        public Manufacturer()
        {
            YearStarted = 1950;
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }
        public int YearStarted { get; set; }

        [Required]
        public Country Country { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}