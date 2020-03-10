using System.Web.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is the class HomeController.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Shows the homepage.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}