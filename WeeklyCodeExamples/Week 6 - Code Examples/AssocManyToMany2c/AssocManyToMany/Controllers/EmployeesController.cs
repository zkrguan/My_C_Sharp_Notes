using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees with Job Duties
        public ActionResult IndexMore()
        {
            return View(m.EmployeeGetAllWithJobDuties());
        }

        // GET: Employees/Details/5
        public ActionResult DetailsWithJobDuties(int? id)
        {
            // Attempt to get the matching object
            var o = m.EmployeeGetByIdWithDetail(id.GetValueOrDefault());

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

        //// GET: Employees/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Employees/Create
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

        // GET: Employees/Edit/5
        public ActionResult EditJobDuties(int? id)
        {
            // Attempt to fetch the matching object
            var employee = m.EmployeeGetByIdWithDetail(id.GetValueOrDefault());

            if (employee == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = m.mapper.Map<EmployeeEditJobDutiesFormViewModel>(employee);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = employee.JobDuties.Select(jd => jd.Id);

                // For clarity, use the named parameter feature of C#
                form.JobDutyList = new MultiSelectList
                    (items: m.JobDutyGetAll(),
                    dataValueField: "Id",
                    dataTextField: "FullName",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult EditJobDuties(int? id, EmployeeEditJobDutiesViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.EmployeeEditJobDuties(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("ByEmployee", "Matches", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("ByEmployeeWithJobDuties", "Matches", new { id = newItem.Id });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
