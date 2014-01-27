using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goldfish.Manager;

namespace Goldfish.Areas.Manager.Controllers
{
	[RouteArea("Manager", AreaPrefix="manager")]
    public class AssetsController : Controller
    {
		[Route("assets.ashx/{*path}")]
        public ActionResult Index(string path) {
			var res = AssetsManager.Instance.Get(path);
			if (res != null) {
				Response.ContentType = res.MimeType;
				Response.BinaryWrite(res.GetData());
			} else {
				Response.StatusCode = 404;
			}
			return null;
        }
	}
}