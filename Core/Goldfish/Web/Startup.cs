using System;
using System.Web.Mvc;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(Goldfish.Web.Startup), "Init")]

namespace Goldfish.Web
{
	/// <summary>
	/// Starts the application automatically.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Starts the web module.
		/// </summary>
		public static void Init() {
			// Initialize the application object
			App.Init();

			// Register Blog module hooks
			Hooks.App.UI.GetMeta += (str) => {
				Helpers.Blog.Meta(str);
			};

			// Set the controler factory
			ControllerBuilder.Current.SetControllerFactory(new Mvc.ControllerFactory());

			// Register the view engine
			Mvc.ViewEngine.Register();
		}
	}
}