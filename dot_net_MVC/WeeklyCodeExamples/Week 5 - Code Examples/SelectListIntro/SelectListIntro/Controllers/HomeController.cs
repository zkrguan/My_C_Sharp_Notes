using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelectListIntro.Models;

namespace SelectListIntro.Controllers
{
    public class HomeController : Controller
    {
        // TODO: 21 - Properties will hold the collections of data
        // Data is in memory only, and is not persisted
        public List<AcademicTerm> AcademicTerms { get; set; }
        public List<Course> Courses { get; set; }

        // Constructor
        public HomeController()
        {
            // Create some other data (see the method at the bottom of this source code file)
            AcademicTerms = new List<AcademicTerm>();
            AcademicTerms.Add(new AcademicTerm { Id = 2174, Year = 2017, Term = "Summer" });
            AcademicTerms.Add(new AcademicTerm { Id = 2177, Year = 2017, Term = "Fall" });
            AcademicTerms.Add(new AcademicTerm { Id = 2181, Year = 2018, Term = "Winter" });

            Courses = new List<Course>();
            Courses.Add(new Course { Id = 47, Code = "BTR490", Name = "Research Internship" });
            Courses.Add(new Course { Id = 49, Code = "BTP500", Name = "Data Structures" });
            Courses.Add(new Course { Id = 55, Code = "BTB520", Name = "Canadian Business Environment" });
            Courses.Add(new Course { Id = 61, Code = "BTS530", Name = "Project Planning" });
            Courses.Add(new Course { Id = 63, Code = "BTH540", Name = "User Interface Design" });
            Courses.Add(new Course { Id = 77, Code = "BTP600", Name = "Design Patterns" });
            Courses.Add(new Course { Id = 79, Code = "BTE620", Name = "Ethics, Law, Professionalism" });
            Courses.Add(new Course { Id = 122, Code = "BTS630", Name = "Project Implementation" });
            Courses.Add(new Course { Id = 132, Code = "BTC640", Name = "Multimedia Design" });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlanCourses()
        {
            // TODO: 27 - Create and configure a view model object

            var form = new CoursePlanForm();

            // TODO: 28 - SelectList objects
            form.AcademicTermList = new SelectList(this.AcademicTerms, "Id", "TermName");
            form.CourseList = new MultiSelectList(this.Courses, "Id", "CourseName");

            // TODO: 29 - Carefully study the PlanCourses view
            return View(form);
        }
    }
}