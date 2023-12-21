using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class JobDutiesController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: JobDuties
        public ActionResult Index()
        {
            return View(m.JobDutyGetAll());
        }

        // GET: Job Duties with Employees
        public ActionResult IndexMore()
        {
            return View(m.JobDutyGetAllWithEmployees());
        }

        // GET: JobDuties/Details/5
        public ActionResult DetailsWithEmployees(int? id)
        {
            // Attempt to get the matching object
            var o = m.JobDutyGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        //// GET: JobDuties/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: JobDuties/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: JobDuties/Edit/5
        public ActionResult EditEmployees(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.JobDutyGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = m.mapper.Map<JobDutyEditEmployeesFormViewModel>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Employees.Select(e => e.Id);

                // For clarity, use the named parameter feature of C#
                form.EmployeeList = new MultiSelectList
                    (items: m.EmployeeGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: JobDuties/Edit/5
        [HttpPost]
        public ActionResult EditEmployees(int? id, JobDutyEditEmployeesViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("JobDutyAndEmployees", "Matches", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("ByJobDuty", "Matches");
            }

            // Attempt to do the update
            var editedItem = m.JobDutyEditEmployees(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("ByJobDuty", "Matches", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("ByJobDutyWithEmployees", "Matches", new { id = newItem.Id });
            }
        }

        // GET: JobDuties/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobDuties/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
