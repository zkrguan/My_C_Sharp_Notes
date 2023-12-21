using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class EmployeeBaseViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Employee name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Office number")]
        public string Office { get; set; }
    }

    public class EmployeeWithJobDutiesViewModel : EmployeeBaseViewModel
    {
        public EmployeeWithJobDutiesViewModel()
        {
            JobDuties = new List<JobDutyBaseViewModel>();
        }

        [Display(Name = "List of job duties")]
        public IEnumerable<JobDutyBaseViewModel> JobDuties { get; set; }
    }

    // ############################################################

    // TODO 06 - Edit job duties for an employee
    // Send TO the HTML Form
    public class EmployeeEditJobDutiesFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Employee name")]
        public string Name { get; set; }

        // TODO 07 - Multiple select requires a MultiSelectList object
        [Display(Name = "Job duties")]
        public MultiSelectList JobDutyList { get; set; }
    }

    // Data submitted by the browser user
    public class EmployeeEditJobDutiesViewModel
    {
        public EmployeeEditJobDutiesViewModel()
        {
            JobDutyIds = new List<int>();
        }

        public int Id { get; set; }

        // TODO 08 - Incoming collection of selected job duty identifiers
        public IEnumerable<int> JobDutyIds { get; set; }
    }
}
