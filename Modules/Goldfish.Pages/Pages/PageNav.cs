using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Goldfish.Pages
{
	/// <summary>
	/// The page nav class is used to build up the hierarchical
	/// navigation structure from the available pages.
	/// </summary>
	[Serializable]
	public class PageNav
	{
		#region Properties
		/// <summary>
		/// Gets the page title.
		/// </summary>
		public string Title { get; internal set; }

		/// <summary>
		/// Gets the page slug.
		/// </summary>
		public string Slug { get; internal set; }

		/// <summary>
		/// Gets the page permalink.
		/// </summary>
		public string Permalink { get; internal set; }

		/// <summary>
		/// Gets/sets the level of the current navigation item.
		/// </summary>
		public int Level { get; internal set; }

		/// <summary>
		/// Gets the available subpages.
		/// </summary>
		public IList<PageNav> SubPages { get; internal set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageNav() {
			SubPages = new List<PageNav>();
		}
	}
}