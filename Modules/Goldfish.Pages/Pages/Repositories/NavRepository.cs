/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Goldfish.Helpers;

namespace Goldfish.Pages.Repositories
{
	/// <summary>
	/// Navigation structure repository.
	/// </summary>
	public class NavRepository
	{
		#region Members
		/// <summary>
		/// The current data context.
		/// </summary>
		private readonly Db uow;
		#endregion

		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="db">The data context</param>
		internal NavRepository(Db db) {
			uow = db;
		}
		/// <summary>
		/// Gets the navigation structure.
		/// </summary>
		/// <returns>The navigation structure</returns>
		public IList<PageNav> Get() {
			var nav = PagesModule.Cache.Get<IList<PageNav>>(PagesModule.CACHE_NAVIGATION_KEY);

			if (nav == null) {
				// Get all of the pages
				var pages = uow.Pages
					.OrderBy(p => p.ParentId)
					.ThenBy(p => p.Seqno).ToList();

				// Sort the pages hierarchical
				nav = Sort(pages.Where(p => p.ParentId == null), pages); ;

				// Add to cache
				PagesModule.Cache.Set(PagesModule.CACHE_NAVIGATION_KEY, nav);
			}
			return nav;
		}

		#region Private methods
		/// <summary>
		/// Sorts the pages recursively into a navigation structure.
		/// </summary>
		/// <param name="level">The current level in the structure.</param>
		/// <param name="all">The complete set of pages</param>
		/// <param name="level">The current level</param>
		/// <returns>The navigation structure.</returns>
		private IList<PageNav> Sort(IEnumerable<Page> pages, IEnumerable<Page> all, int level = 1) {
			var nav = new List<PageNav>();

			foreach (var page in pages) {
				// Create the new navigation element
				var pn = new PageNav() { 
					Title = page.Title,
					Slug = page.Slug,
					Permalink = page.GetPermalink(),
					Level = level
				};
				// Sort the subpages
				pn.SubPages = Sort(all.Where(p => p.ParentId == p.Id), all, level + 1);

				// Add to structure
				nav.Add(pn);
			}
			return nav;
		}
		#endregion
	}
}