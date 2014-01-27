using System;
using System.ComponentModel.Composition;
using System.Web;
using Goldfish.Extend;

namespace Goldfish.Feed
{
	/// <summary>
	/// Starts the feed module.
	/// </summary>
	[Module(Name="Feeds")]
	[Export(typeof(IModule))]
	public class FeedModule : Module
	{
		/// <summary>
		/// Initializes the feed module.
		/// </summary>
		public override void Init() {
			Goldfish.Hooks.App.UI.GetMeta += str => { 
				str.AppendLine("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/rss") + "/" + "\" />");
				str.AppendLine("<link rel=\"alternate\" type=\"application/atom+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/atom") + "/" + "\" />");
			};
		}
	}
}