using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SelectListIntro.Models
{
    // This source code file will hold "...Form" view model classes

    // TODO: 11 - View model for an "add new" form, Course entity
    public class CoursePlanForm
    {
        [Display(Name ="Student name")]
        public string Name { get; set; }

        // SelectList for AcademicTerm
        [Display(Name ="Academic term")]
        public SelectList AcademicTermList { get; set; }

        // SelectList for Course collection
        [Display(Name = "Desired course(s)")]
        public MultiSelectList CourseList { get; set; }
    }

    // ############################################################
    // The essential view model classes for objects...

    // TODO: 12 - View model class for an Academic Term entity
    public class AcademicTerm
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Term { get; set; }

        // Assemble a nicer-looking value
        // Notice that it's read-only - it has a getter, but no setter
        public string TermName
        {
            get { return string.Format("{0} - {1}", this.Year, this.Term); }
        }
    }

    // TODO: 13 - View model class for a Course entity
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        // Assemble a nicer-looking value
        // Notice that it's read-only - it has a getter, but no setter
        public string CourseName
        {
            get { return string.Format("{0} - {1}", this.Code, this.Name); }
        }
    }

}
