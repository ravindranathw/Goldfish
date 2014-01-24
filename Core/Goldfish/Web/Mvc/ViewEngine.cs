using System;
using System.Web.Mvc;

namespace Goldfish.Web.Mvc
{
	/// <summary>
	/// The goldfish view engine that fetches all views from the current theme.
	/// </summary>
	public sealed class ViewEngine : RazorViewEngine
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ViewEngine() : base() {
			ViewLocationFormats = new[] {
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.vbhtml",
	            "~/Themes/" + Config.App.Theme + "/Views/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/{0}.vbhtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.vbhtml",
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{1}/{0}.vbhtml",
				"~/Views/{0}.cshtml",
				"~/Views/{0}.vbhtml",
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Shared/{0}.vbhtml"
	        };

			MasterLocationFormats = new[] {
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.vbhtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.vbhtml",
	            "~/Views/{1}/{0}.cshtml",
	            "~/Views/{1}/{0}.vbhtml",
	            "~/Views/Shared/{0}.cshtml",
	            "~/Views/Shared/{0}.vbhtml"
	        };

			PartialViewLocationFormats = new[] {
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/{1}/{0}.vbhtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.cshtml",
	            "~/Themes/" + Config.App.Theme + "/Views/Shared/{0}.vbhtml",
	            "~/Views/{1}/{0}.cshtml",
	            "~/Views/{1}/{0}.vbhtml",
	            "~/Views/Shared/{0}.cshtml",
	            "~/Views/Shared/{0}.vbhtml"
	        };
		}

		/// <summary>
		/// Registers the goldfish view engine.
		/// </summary>
		public static void Register() {
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new ViewEngine());
		}
	}
}