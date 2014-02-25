/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Web;

namespace Goldfish.Helpers
{
	public static class Pages
	{
		/// <summary>
		/// Gets the current navigation structure.
		/// </summary>
		public static IList<Goldfish.Pages.PageNav> Navigation {
			get {
				using (var api = new Goldfish.Pages.Api()) {
					return api.Navigation.Get();
				}
			}
		}

		#region Page extensions
		/// <summary>
		/// Gets the permalink for the page.
		/// </summary>
		/// <param name="post">The current page</param>
		/// <returns>The permalink</returns>
		public static string GetPermalink(this Goldfish.Pages.Page page) {
			return Utils.Url("~/" + page.Slug);
		}

		/// <summary>
		/// Gets the absolute URL for the page.
		/// </summary>
		/// <param name="post">The current page</param>
		/// <returns>The url</returns>
		public static string GetAbsoluteUrl(this Goldfish.Pages.Page page) {
			return Utils.AbsoluteUrl("~/" + page.Slug);
		}
		#endregion
	}
}