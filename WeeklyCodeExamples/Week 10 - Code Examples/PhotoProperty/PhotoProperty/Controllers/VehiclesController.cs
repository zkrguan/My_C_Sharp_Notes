﻿using PhotoProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoProperty.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Vehicles
        public ActionResult Index()
        {
            return View(m.VehicleGetAll());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.VehicleGetById(id.GetValueOrDefault());

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
        [Authorize(Roles = "Engineering")]
        public ActionResult Create()
        {
            var form = new VehicleAddFormViewModel();

            return View(form);

            // TODO 5 - Make sure you remember to change the multipart "enctype" value in the HTML Form in the view
        }

        // POST: Vehicles/Create
        [HttpPost]
        [Authorize(Roles = "Engineering")]
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
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }
    }
}
