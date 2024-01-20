using System;
using System.ComponentModel.DataAnnotations;

namespace CustomStoreForCarBusiness.Data
{
    // TODO 04 - Vehicle design model class
    public class Vehicle
    {
        public Vehicle()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        public int ModelYear { get; set; }

        public int MSRP { get; set; }

        [Required]
        public Manufacturer Manufacturer { get; set; }
    }
}