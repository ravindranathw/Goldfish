﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Goldfish.Feed.Controllers
{
	[RoutePrefix("feed")]
    public class FeedController : Controller
    {
		#region Members
		private readonly Goldfish.IApi Api;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="api">The api object</param>
		public FeedController(Goldfish.IApi api) {
			Api = api;
		}

		/// <summary>
		/// Gets the rss feed for the specified resource.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("rss")]
		public async Task<ActionResult> Rss() {
			return new RssResult((await Api.Posts.GetAsync()));
		}

		/// <summary>
		/// Gets the atom feed for the specified resource.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("atom")]
		public async Task<ActionResult> Atom() {
			return new AtomResult((await Api.Posts.GetAsync()));
		}
	}
}