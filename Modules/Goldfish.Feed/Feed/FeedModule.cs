﻿using System;
using System.ComponentModel.Composition;
using System.Web;

namespace Goldfish.Feed
{
	/// <summary>
	/// Starts the feed module.
	/// </summary>
	[Export(typeof(Goldfish.Extend.IModule))]
	public class FeedModule : Goldfish.Extend.IModule
	{
		/// <summary>
		/// Initializes the feed module.
		/// </summary>
		public void Init() {
			Goldfish.Hooks.App.UI.GetMeta += str => { 
				str.AppendLine("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/rss") + "/" + "\" />");
				str.AppendLine("<link rel=\"alternate\" type=\"application/atom+xml\" title=\"" +
					Config.Blog.Title + "\" href=\"" + Utils.AbsoluteUrl("~/feed/atom") + "/" + "\" />");
			};
		}
	}
}