using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Blog
{
	public static class Tools
	{
		/// <summary>
		/// Gets the currently requested model in the current http context.
		/// </summary>
		internal static object Current {
			get { return HttpContext.Current.Items["Blog_Current"]; }
			set { HttpContext.Current.Items["Blog_Current"] = value; }
		}

		/// <summary>
		/// Gets if the current request is for a single post.
		/// </summary>
		public static bool IsPost {
			get {
				var current = Current;
				return current != null && current is Models.Post;
			}
		}

		/// <summary>
		/// Gets if the current request is for an archive.
		/// </summary>
		public static bool IsArchive {
			get {
				var current = Current;
				return current != null & current is Models.ArchiveBase;
			}
		}
	}
}