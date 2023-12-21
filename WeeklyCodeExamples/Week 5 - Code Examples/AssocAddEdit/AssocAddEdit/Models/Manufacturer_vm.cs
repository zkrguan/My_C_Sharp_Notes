﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocAddEdit.Models
{
    // TODO: 11 - Manufacturer view model classes
    // Study the All_vm class diagram and its "readme" text, for more info/explanations

    public class ManufacturerBaseViewModel
    {
        public ManufacturerBaseViewModel()
        {
            YearStarted = 1950;
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Manufacturer Name")]
        public string Name { get; set; }

        [Range(1850, Int16.MaxValue)]
        [Display(Name = "Year Started")]
        public int YearStarted { get; set; }
    }

    // TODO: 12 - Notice this "with detail" view model class includes a collection of vehicles
    public class ManufacturerWithDetailViewModel : ManufacturerBaseViewModel
    {
        public ManufacturerWithDetailViewModel()
        {
            Vehicles = new List<VehicleBaseViewModel>();
        }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public IEnumerable<VehicleBaseViewModel> Vehicles { get; set; }
    }
}
