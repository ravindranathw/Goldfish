using System;
using System.Web;
using System.Web.Mvc;

namespace Goldfish.Areas.Manager.Controllers
{
	/// <summary>
	/// The dashboard controller shows global information about
	/// the current installation.
	/// </summary>
	[Authorize(Roles="Admin")]
	[RouteArea("Manager", AreaPrefix="manager")]
    public class DashboardController : Controller
    {
		/// <summary>
		/// Gets the start view for the dashboard.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("")]
        public ActionResult Start() {
            return View();
        }
	}
}