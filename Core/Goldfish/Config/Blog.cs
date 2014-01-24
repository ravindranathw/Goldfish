using System;

namespace Goldfish.Config
{
	/// <summary>
	/// Configuration parameters for the blog module.
	/// </summary>
	public static class Blog
	{
		/// <summary>
		/// Gets/sets the blog title.
		/// </summary>
		public static string Title {
			get { return Utils.GetParam<string>("BLOG_TITLE", s => s); }
			set { Utils.SetParam("BLOG_TITLE", value); }
		}

		/// <summary>
		/// Gets/sets the blog description.
		/// </summary>
		public static string Description {
			get { return Utils.GetParam<string>("BLOG_DESCRIPTION", s => s); }
			set { Utils.SetParam("BLOG_DESCRIPTION", value); }
		}

		/// <summary>
		/// Gets/sets the meta keywords for the blog.
		/// </summary>
		public static string MetaKeywords {
			get { return Utils.GetParam<string>("BLOG_META_KEYWORDS", s => s); }
			set { Utils.SetParam("BLOG_META_KEYWORDS", value); }
		}

		/// <summary>
		/// Gets/sets the meta decription for the blog.
		/// </summary>
		public static string MetaDescription {
			get { return Utils.GetParam<string>("BLOG_META_DESCRIPTION", s => s); }
			set { Utils.SetParam("BLOG_META_DESCRIPTION", value); }
		}

		/// <summary>
		/// Gets/sets the archive page size.
		/// </summary>
		public static int ArchivePageSize {
			get { return Utils.GetParam<int>("ARCHIVE_PAGE_SIZE", s => Convert.ToInt32(s)); }
			set { Utils.SetParam("ARCHIVE_PAGE_SIZE", value); }
		}

		/// <summary>
		/// The different parameters available for comments
		/// </summary>
		public static class Comments 
		{
			/// <summary>
			/// Gets/sets if the active comment type.
			/// </summary>
			public static Models.CommentType Type {
				get { return Utils.GetParam<Models.CommentType>("COMMENT_TYPE", s => (Models.CommentType)Enum.Parse(typeof(Models.CommentType), s)); }
				set { Utils.SetParam("COMMENT_ENABLED", value); }
			}

			/// <summary>
			/// Gets/sets if comments from authorized users should be moderated.
			/// </summary>
			public static bool ModerateAuthorized {
				get { return Utils.GetParam<bool>("COMMENT_MODERATE_AUTHORIZED", s => Convert.ToBoolean(s)); }
				set { Utils.SetParam("COMMENT_MODERATE_AUTHORIZED", value); }
			}

			/// <summary>
			/// Gets/sets if comments from anonymous users should be moderated.
			/// </summary>
			public static bool ModerateAnonymous {
				get { return Utils.GetParam<bool>("COMMENT_MODERATE_ANONYMOUS", s => Convert.ToBoolean(s)); }
				set { Utils.SetParam("COMMENT_MODERATE_ANONYMOUS", value); }
			}

			/// <summary>
			/// Gets/sets the configured Disqus id of Disqus comments are used.
			/// </summary>
			public static string DisqusId {
				get { return Utils.GetParam<string>("COMMENT_DISQUS_ID", s => s); }
				set { Utils.SetParam("COMMENT_DISQUS_ID", value); }
			}
		}
	}
}