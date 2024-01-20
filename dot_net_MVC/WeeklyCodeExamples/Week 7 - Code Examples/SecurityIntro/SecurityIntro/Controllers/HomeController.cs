using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityIntro.Controllers
{
    // TODO 01 - Protect all methods in this controller
    // Request must be authenticated
    [Authorize]
    public class HomeController : Controller
    {
        // TODO 06 - Override the controller-level protection
        // Allow this action/method to run for all requests
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // This method is protected
        // TODO 07 - Inherited from the controller-level protection
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Override the controller-level protection
        // TODO 08 - Allow only specific users to run the action/method
        [Authorize(Users = "john@example.com,mary@example.com")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}