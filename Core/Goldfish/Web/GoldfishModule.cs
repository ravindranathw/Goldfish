using System;
using System.Web;

namespace Goldfish.Web
{
	/// <summary>
	/// The main goldfish http module.
	/// </summary>
	public class GoldfishModule : IHttpModule
	{
		/// <summary>
		/// Disposes the http module.
		/// </summary>
		public void Dispose() {
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Initializes the goldfish http module.
		/// </summary>
		/// <param name="context">The application context</param>
		public void Init(HttpApplication context) {
			// Register begin request 
			context.BeginRequest += (sender, e) => {
				if (Hooks.App.Request.OnBeginRequest != null)
					Hooks.App.Request.OnBeginRequest(sender, e);
			};

			// Register end request
			context.EndRequest += (sender, e) => { 
				if (Hooks.App.Request.OnEndRequest != null)
					Hooks.App.Request.OnEndRequest(sender, e);
			};
		}
	}
}