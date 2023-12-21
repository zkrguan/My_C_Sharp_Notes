using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTMLFormFoundations.Controllers
{
    // TODO: 01 - All the interesting code can be found in the views (*.cshtml)

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/SimpleInput
        public ActionResult SimpleInput()
        {
            // TODO: 06 - Display a view with simple <input> elements
            return View();
        }

        // GET: /Home/DropdownListboxSimple
        public ActionResult DropdownListboxSimple()
        {
            // TODO: 11 - Display items from a string collection in <select> elements

            // Create a collection of strings
            // They will be rendered in an item-selection control
            var colourNames = new List<string> { "Red", "Green", "Blue" };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<string>
            return View(colourNames);
        }

        // GET: /Home/DropdownListboxObject
        public ActionResult DropdownListboxObject()
        {
            // TODO: 12 - Display items from a "Teacher" collection in <select> elements

            // Create a collection of "Teacher" objects (the class is defined below)
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Peter McIntyre", Age = 39 },
                new Teacher { Id = 2, Name = "Ian Tipson", Age = 42 },
                new Teacher { Id = 3, Name = "Wei Song", Age = 35 }
            };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<HTMLFormFoundations.Controllers.Teacher>
            return View(teachers);
        }

        // GET: /Home/RadioCheckSimple
        public ActionResult RadioCheckSimple()
        {
            // TODO: 16- Display items from a string collection in <input type=radio... and <input type=checkbox elements

            // Create a collection of strings
            // They will be rendered in an item-selection control
            var colourNames = new List<string> { "Red", "Green", "Blue" };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<string>
            return View(colourNames);
        }

        // GET: /Home/RadioCheckObject
        public ActionResult RadioCheckObject()
        {
            // TODO: 17 - Display items from a "Teacher" collection in <input type=radio... and <input type=checkbox elements

            // Create a collection of "Teacher" objects (the class is defined below)
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Peter McIntyre", Age = 39 },
                new Teacher { Id = 2, Name = "Ian Tipson", Age = 42 },
                new Teacher { Id = 3, Name = "Wei Song", Age = 35 }
            };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<HTMLFormFoundations.Controllers.Teacher>
            return View(teachers);
        }
    }

    // TODO: 02 - This is the Teacher class that's used in the "object" examples
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}