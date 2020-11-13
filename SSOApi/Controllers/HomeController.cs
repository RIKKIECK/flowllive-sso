using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSOApi.Controllers
{
    /// <summary>
    /// home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// vista home index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Titulo de la página principal
            ViewBag.Title = "Home Page";
            

            return View();
        }
    }
}