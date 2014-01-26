using System;
using System.Web;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(Goldfish.Feed.Startup), "Init")]

namespace Goldfish.Feed
{
	/// <summary>
	/// Starts the feed module.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes the feed module.
		/// </summary>
		public static void Init() {
			var context = HttpContext.Current;
			var path = context.Server.MapPath("~/App_Data/FilePoster");

			Goldfish.Hooks.App.UI.GetMeta += str => { 
				str.AppendLine("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/rss") + "/" + "\" />");
				str.AppendLine("<link rel=\"alternate\" type=\"application/atom+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/atom") + "/" + "\" />");
			};
		}
	}
}