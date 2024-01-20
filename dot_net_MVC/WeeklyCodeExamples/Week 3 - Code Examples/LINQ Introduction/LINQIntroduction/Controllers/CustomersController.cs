﻿using LINQIntroduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQIntroduction.Controllers
{
    public class CustomersController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Customers
        public ActionResult Index()
        {
            // Fetch the collection
            var c = m.CustomerGetAll();

            // Pass the collection to the view
            return View(c);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

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

        // TODO: 06 - New method that sorts (orders) the results
        // We could have done the "order by" work in the Manager class (and should in the future)
        
        // GET: Customers/Sorted
        public ActionResult Sorted()
        {
            // Fetch the collection
            var c = m.CustomerGetAll();

            var sorted = c.OrderBy(ln => ln.LastName).ThenBy(fn => fn.FirstName);

            // Pass the collection to the view, sorted
            return View(sorted);
        }

        // TODO: 07 - New method pair that enables "search by country"
        // Uses a new view model class that holds the search term
        // The view was created with the "Create" template
        // GET: Customers/SearchByCountry
        public ActionResult SearchByCountry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchByCountry(SearchByNameViewModel item)
        {
            // Fetch the collection
            var c = m.CustomerGetAllByCountry(item.Name);

            // Pass the collection to the view, sorted
            // Now do you understand why we can (and should) do the "order by" work in the Manager class?
            // If there's a natural order to the results, then do it
            return View("index", c.OrderBy(ln => ln.LastName).ThenBy(fn => fn.FirstName));
        }

        // TODO: 08 - New method pair that enables "search by name"
        // Uses a new view model class that holds the search term
        // The view was created with the "Create" template
        // GET: Customers/SearchByName
        public ActionResult SearchByName()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchByName(SearchByNameViewModel item)
        {
            // Fetch the sorted collection, and pass it to the view
            return View("index", m.CustomerGetAllByName(item.Name));
        }


    }
}
