using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class JobDutyBaseViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Job duty name")]
        public string Name { get; set; }

        [Required, StringLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // TODO 21 - New property, composed name + description
        // Getter only, no setter (i.e. it's read only)
        // Notice the use of C# 6.0 string interpolation feature
        [Display(Name = "Job duty and description")]
        public string FullName
        {
            get
            {
                return $"{Name} - {Description}";
            }
        }
    }

    public class JobDutyWithEmployeesViewModel : JobDutyBaseViewModel
    {
        public JobDutyWithEmployeesViewModel()
        {
            Employees = new List<EmployeeBaseViewModel>();
        }

        [Display(Name = "Employees with this job duty")]
        public IEnumerable<EmployeeBaseViewModel> Employees { get; set; }
    }

    // TODO 22 - Edit employees with a job duty
    // Send TO the HTML Form
    public class JobDutyEditEmployeesFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Job duty name")]
        public string Name { get; set; }

        // TODO 23 - Multiple select requires a MultiSelectList object
        public MultiSelectList EmployeeList { get; set; }
    }

    // Data submitted by the browser user
    public class JobDutyEditEmployeesViewModel
    {
        public JobDutyEditEmployeesViewModel()
        {
            EmployeeIds = new List<int>();
        }

        public int Id { get; set; }

        // TODO 24 - Incoming collection of selected employee identifiers
        public IEnumerable<int> EmployeeIds { get; set; }
    }
}
