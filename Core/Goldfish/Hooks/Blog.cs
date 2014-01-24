using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Hooks
{
	/// <summary>
	/// The hooks available for the blog module.
	/// </summary>
	public static class Blog
	{
		/// <summary>
		/// The hooks available for the models.
		/// </summary>
		public static class Model
		{ 
			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
			public static Delegates.ModelDelegate<Models.Author> OnAuthorLoad;

			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
 			public static Delegates.ModelDelegate<Models.Category> OnCategoryLoad;

			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
			public static Delegates.ModelDelegate<Models.Post> OnPostLoad;

			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
			public static Delegates.ModelDelegate<Models.Tag> OnTagLoad;
		}

		/// <summary>
		/// The hooks available for UI rendering.
		/// </summary>
		public static class UI
		{ 
			/// <summary>
			/// Called when the blog has finished rendering the meta tags for an archive 
			/// page. The output of this method is appended to the default data.
			/// </summary>
			public static Delegates.OutputAppendDelegate GetArchiveMeta;

			/// <summary>
			/// Called when the blog has finished rendering the meta tags for a single 
			/// post. The output of this method is appended to the default data.
			/// </summary>
			public static Delegates.OutputAppendDelegate GetPostMeta;

			/// <summary>
			/// Called for each category when the method GetCategories is executed on the post
			/// model. If this hook is implemented the default output is replaced.
			/// </summary>
			public static Delegates.OutputAppendDelegate<Models.Category> GetPostCategory;

			/// <summary>
			/// Called for each tag when the method GetTags is executed on the post
			/// model. If this hook is implemented the default output is replaced.
			/// </summary>
			public static Delegates.OutputAppendDelegate<Models.Tag> GetPostTag;

			/// <summary>
			/// Called when the extension method GetExcerpt is executed on the post model. If
			/// this hooks is implemented the default output is replaced.
			/// </summary>
			public static Delegates.OutputReturnDelegate<Models.Post> GetPostExcerpt;

			/// <summary>
			/// Called when the extension method GetPublished is executed on the post model. If
			/// this hooks is implemented the default output is replaced.
			/// </summary>
			public static Delegates.OutputReturnDelegate<Models.Post> GetPostPublished;

			/// <summary>
			/// Called when the extension method GetPermalink is executed on the post model. If
			/// this hooks is implemented the default output is replaced.
			/// </summary>
			public static Delegates.OutputReturnDelegate<Models.Post> GetPostPermalink;
		}
	}
}