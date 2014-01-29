using System;
using System.IO;
using System.Web.Mvc;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(Goldfish.Startup), "PreInit")]
[assembly: PostApplicationStartMethod(typeof(Goldfish.Startup), "Init")]

namespace Goldfish
{
	/// <summary>
	/// Starts the application automatically.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes the pre startup events.
		/// </summary>
		public static void PreInit() { 
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Web.GoldfishModule));
		}

		/// <summary>
		/// Starts the web module.
		/// </summary>
		public static void Init() {
			var context = System.Web.HttpContext.Current;
			var path = context.Server.MapPath("~/App_Data/Blog");

			// Initialize the application object
			App.Init();

			// Ensure blog directory
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			// Register Blog module hooks
			Hooks.App.UI.GetMeta += (str) => {
				Helpers.Blog.Meta(str);
			};

			// Set the controler factory
			ControllerBuilder.Current.SetControllerFactory(new Web.Mvc.ControllerFactory());

			// Register the view engine
			Web.Mvc.ViewEngine.Register();
		}
	}
}