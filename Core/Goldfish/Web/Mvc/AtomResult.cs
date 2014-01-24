using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;

namespace Goldfish.Web.Mvc
{
	/// <summary>
	/// Action result for atom feeds.
	/// </summary>
	public sealed class AtomResult : SyndicationResult
	{
		#region Properties
		/// <summary>
		/// Gets the content type of the current feed.
		/// </summary>
		protected override string ContentType {
			get { return "application/atom+xml"; }
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="posts">The current posts</param>
		public AtomResult(IEnumerable<Models.Post> posts) : base(posts) { }

		/// <summary>
		/// Gets the current formatter.
		/// </summary>
		/// <returns>The formatter</returns>
		protected override SyndicationFeedFormatter GetFormatter(SyndicationFeed feed) {
			return new Atom10FeedFormatter(feed);
		}
	}
}