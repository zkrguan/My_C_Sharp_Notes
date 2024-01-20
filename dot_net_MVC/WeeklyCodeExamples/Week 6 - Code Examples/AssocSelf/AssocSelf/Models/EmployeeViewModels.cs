using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocSelf.Controllers
{
    // Base class with many of the properties from the design model
    public class EmployeeBaseViewModel
    {
        public EmployeeBaseViewModel()
        {
            BirthDate = DateTime.Now.AddYears(-25);
            HireDate = DateTime.Now;
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // TODO 11 - FullName composed property
        [Display(Name = "Employee name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Hire date")]
        public DateTime? HireDate { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
    }

    public class EmployeeWithOrgInfoViewModel : EmployeeBaseViewModel
    {
        public EmployeeWithOrgInfoViewModel()
        {
            DirectReports = new List<EmployeeBaseViewModel>();
        }

        // Self-referencing to-one properties
        // If YOU were writing this class yourself, you should use better names
        [Display(Name = "Reports to")]
        public int? ReportsToId { get; set; }

        [Display(Name = "Reports to")]
        public EmployeeBaseViewModel ReportsTo { get; set; }

        // Self-referencing to-many property
        // If YOU were writing this class yourself, you should use a better name
        // The collection should be named "DirectReports" (plural)
        [Display(Name = "Direct reports")]
        public IEnumerable<EmployeeBaseViewModel> DirectReports { get; set; }
    }

    // ############################################################
    // Edit the supervisor

    // TODO 12 - Edit the supervisor
    // Send this TO the HTML Form
    public class EmployeeEditSupervisorFormViewModel
    {
        public EmployeeEditSupervisorFormViewModel()
        {
            CurrentSupervisor = "(none)";
        }

        // Identification properties

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // Current supervisor
        [Display(Name = "Current supervisor")]
        public string CurrentSupervisor { get; set; }

        // Will allow the job title to be edited

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        // TODO 13 - SelectList for the supervisor
        [Display(Name = "New supervisor")]
        public SelectList EmployeeList { get; set; }
    }

    // For the data submitted by the browser user
    public class EmployeeEditSupervisorViewModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        // TODO 14 - Identifier for the supervisor
        public int ReportsToId { get; set; }
    }

    // ############################################################
    // Edit the direct reports

    // TODO 21 - Edit the direct reports
    // Send this TO the HTML Form
    public class EmployeeEditDirectReportsFormViewModel
    {
        // Identification properties

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // TODO 22 - FullName composed property
        [Display(Name = "Employee name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }

        // TODO 23 - Current direct reports
        [Display(Name = "Current direct reports")]
        public IEnumerable<EmployeeBaseViewModel> DirectReports { get; set; }

        // TODO 24 - MultiSelectList for the direct reports
        [Display(Name = "New direct reports")]
        public MultiSelectList EmployeeList { get; set; }
    }

    public class EmployeeEditDirectReportsViewModel
    {
        public EmployeeEditDirectReportsViewModel()
        {
            EmployeeIds = new List<int>();
        }

        [Key]
        public int EmployeeId { get; set; }

        // TODO 25 - Collection of selected identifiers
        public IEnumerable<int> EmployeeIds { get; set; }
    }

}
