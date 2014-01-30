/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace Goldfish.Feed
{
	/// <summary>
	/// Base class for action results returning a syndication feed.
	/// </summary>
	public abstract class SyndicationResult : ActionResult
	{
		#region Members
		/// <summary>
		/// The protected post collection
		/// </summary>
		protected readonly IEnumerable<Models.Post> Posts;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the content type of the current feed.
		/// </summary>
		protected abstract string ContentType { get; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="posts">The current posts</param>
		public SyndicationResult(IEnumerable<Models.Post> posts) : base() {
			Posts = posts;
		}

		/// <summary>
		/// Executes the syndication result on the given context.
		/// </summary>
		/// <param name="context">The current context.</param>
		public override void ExecuteResult(ControllerContext context) {
			var response = context.HttpContext.Response;
			var writer = new XmlTextWriter(response.OutputStream, Encoding.UTF8);

			// Write headers
			response.ContentType = ContentType;
			response.ContentEncoding = Encoding.UTF8;

			var feed = new SyndicationFeed() { 
				Title = new TextSyndicationContent(Config.Blog.Title),
				LastUpdatedTime = Posts.First().Published.Value,
				Description = new TextSyndicationContent(Config.Blog.Description),
			};
			feed.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(Utils.AbsoluteUrl("~/"))));

			var items = new List<SyndicationItem>();
			foreach (var post in Posts) {
				var item = new SyndicationItem() { 
					Title = SyndicationContent.CreatePlaintextContent(post.Title),
					PublishDate = post.Published.Value,
					Summary = SyndicationContent.CreateHtmlContent(post.Html.ToHtmlString())
				};
				item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(Utils.AbsoluteUrl("~/" + post.Slug))));
				items.Add(item);
			}
			feed.Items = items;

			var formatter = GetFormatter(feed);
			formatter.WriteTo(writer);

			writer.Flush();
			writer.Close();
		}

		#region Abstract methods
		/// <summary>
		/// Gets the current formatter.
		/// </summary>
		/// <param name="feed">The feed</param>
		/// <returns>The formatter</returns>
		protected abstract SyndicationFeedFormatter GetFormatter(SyndicationFeed feed);
		#endregion
	}
}