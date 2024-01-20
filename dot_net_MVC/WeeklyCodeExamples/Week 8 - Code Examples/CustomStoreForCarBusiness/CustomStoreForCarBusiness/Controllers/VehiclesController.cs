﻿using CustomStoreForCarBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomStoreForCarBusiness.Controllers
{
    // Protect all actions/methods
    [Authorize]
    public class VehiclesController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // TODO 31 - Vehicles controller, full functionality

        // GET: Vehicles
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(m.VehicleGetAllWithDetail());
        }

        // GET: Vehicles/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.VehicleGetByIdWithDetail(id.GetValueOrDefault());

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

        // GET: Vehicles/Create
        // TODO 32 - Allow a Manager to use this "add new" action/method pair
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            // Create a form
            var form = new VehicleAddFormViewModel();
            // Configure the SelectList for the item-selection element on the HTML Form
            form.ManufacturerList = new SelectList(m.ManufacturerGetAll(), "Id", "Name");

            return View(form);
        }

        // POST: Vehicles/Create
        // TODO 33 - Allow a Manager to use this "add new" action/method pair
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Create(VehicleAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.VehicleAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // GET: Vehicles/Edit/5
        // TODO 33 - Can be edited by different kinds of users
        // TODO 34 - Comma-separated list of roles is an "OR" condition
        [Authorize(Roles = "Manager,SalesRep,ClericalSupport")]
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.VehicleGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an "edit form"

                // Notice that o is a ...Base object
                // We must map it to a ...Form object
                var form = m.mapper.Map<VehicleEditFormViewModel>(o);

                return View(form);
            }
        }

        // POST: Vehicles/Edit/5
        // Can be edited by different kinds of users
        // Comma-separated list of roles is an "OR" condition
        [Authorize(Roles = "Manager,SalesRep,ClericalSupport")]
        [HttpPost]
        public ActionResult Edit(int? id, VehicleEditViewModel newItem)
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
            var editedItem = m.VehicleEditMSRP(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id });
            }
        }

        // GET: Vehicles/Delete/5
        // TODO 35 - Vehicle "delete item" is a more serious matter
        // Must be a vice-president who manages people and resources
        // TODO 36 - Stacked statements is an "AND" condition
        [Authorize(Roles = "VicePresident")]
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.VehicleGetByIdWithDetail(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Vehicles/Delete/5
        // Vehicle "delete item" is a more serious matter
        // Must be a vice-president who manages people and resources
        // Stacked statements is an "AND" condition
        [Authorize(Roles = "VicePresident")]
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.VehicleDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }
    }
}
