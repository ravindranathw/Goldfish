using System;
using System.Web;
using System.Web.Mvc;

namespace Goldfish.Areas.Manager.Controllers
{
	/// <summary>
	/// The account controller handles authentication for
	/// the manager interface.
	/// </summary>
	[RouteArea("Manager", AreaPrefix="manager")]
    public class AccountController : Controller
    {
		/// <summary>
		/// Gets the login view.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("login")]
        public ActionResult Login() {
            return View();
        }

		/// <summary>
		/// Authenticates the user with the given credentials.
		/// </summary>
		/// <param name="model">The login model</param>
		/// <returns>The action result</returns>
		[Route("login")]
		[HttpPost]
		public ActionResult Login(Models.Account.LoginModel model) {
			if (ModelState.IsValid) {
				return RedirectToAction("Start", "Dashboard");
			}
			return View("Login");
		}

		/// <summary>
		/// Logs out the currently authenticated user.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("logout")]
		public ActionResult Logout() {
			return RedirectToAction("Login");
		}
	}
}