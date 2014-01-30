/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using System.Web.WebPages;
using RazorGenerator.Mvc;

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
			// Clear the view enginges collection
			ViewEngines.Engines.Clear();

			// Create the theme view engines
			ViewEngines.Engines.Add(new ViewEngine());

			var assemblies = new List<Assembly>();

			// Check if any modules have registered for precompiled views.
			if (Hooks.App.Init.RegisterPrecompiledViews != null)
				Hooks.App.Init.RegisterPrecompiledViews(assemblies);

			// Create precompiled view engines for all requested modules.
			foreach (var assembly in assemblies) { 
				var engine = new PrecompiledMvcEngine(assembly) {
					UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
				};
				ViewEngines.Engines.Add(engine);
				VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
			}
		}
	}
}