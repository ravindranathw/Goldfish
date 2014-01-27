using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goldfish.Areas.Manager.Controllers
{
	[RouteArea("Manager", AreaPrefix="manager")]
    public class ManagerController : Controller
    {
		[Route("settings/{module?}")]
		public ActionResult Settings(string module = "") {
			return View(App.Instance.Modules.Where(m => m.Value.HasConfig).Select(m => m.Value).ToList());
		}
	}
}