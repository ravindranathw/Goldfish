using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Goldfish.Helpers
{
	public static class Blog
	{
		/// <summary>
		/// Gets the blog title.
		/// </summary>
		public static string Title { get { return Config.Blog.Title; } }

		/// <summary>
		/// Gets the blog description.
		/// </summary>
		public static string Description { get { return Config.Blog.Description; } }

		/// <summary>
		/// Gets if comments are enabled.
		/// </summary>
		public static bool CommentsEnabled { get { return Config.Blog.Comments.Type == Models.CommentType.Enabled; } }

		/// <summary>
		/// Gets if comments from Disqus are enabled.
		/// </summary>
		public static bool DisqusEnabled { get { return Config.Blog.Comments.Type == Models.CommentType.Disqus; } }

		/// <summary>
		/// Generates meta information for blog entries.
		/// </summary>
		/// <param name="str">The current string builder</param>
		internal static void Meta(StringBuilder str) { 
			var current = Goldfish.Blog.Tools.Current;

			// Cast current model
			var post = current != null && current is Models.Post ? (Models.Post)current : null;
			var archive = current != null && current is Models.ArchiveBase ? (Models.ArchiveBase)current : null;

			// Keywords & description
			if (post != null) {
				if (!String.IsNullOrEmpty(post.Keywords))
					str.AppendLine("<meta name=\"keywords\" content=\"" + post.Keywords + "\" />");
				if (!String.IsNullOrEmpty(post.Description))
					str.AppendLine("<meta name=\"description\" content=\"" + post.Description + "\" />");

			} else if (archive != null) { 
				if (!String.IsNullOrEmpty(Config.Blog.MetaKeywords))
					str.AppendLine("<meta name=\"keywords\" content=\"" + Config.Blog.MetaKeywords + "\" />");
				if (!String.IsNullOrEmpty(Config.Blog.MetaDescription))
					str.AppendLine("<meta name=\"description\" content=\"" + Config.Blog.MetaDescription + "\" />");
			}

			// Open graph
			str.AppendLine("<meta property=\"og:site_name\" content=\"" + Config.Blog.Title + "\" />");
			if (post != null) {
				str.AppendLine("<meta property=\"og:url\" content=\"" + Utils.AbsoluteUrl("~/" + post.Slug) + "\" />");
				str.AppendLine("<meta property=\"og:type\" content=\"article\" />");
				str.AppendLine("<meta property=\"og:title\" content=\"" + post.Title + "\" />");
				if (!String.IsNullOrEmpty(post.Description))
					str.AppendLine("<meta property=\"og:description\" content=\"" + post.Description + "\" />");
			} else if (archive != null) { 
				str.AppendLine("<meta property=\"og:url\" content=\"" + Utils.AbsoluteUrl("~/") + "\" />");
				str.AppendLine("<meta property=\"og:type\" content=\"website\" />");
				str.AppendLine("<meta property=\"og:title\" content=\"" + Config.Blog.Title + "\" />");
				if (!String.IsNullOrEmpty(Config.Blog.MetaDescription))
					str.AppendLine("<meta property=\"og:description\" content=\"" + Config.Blog.MetaDescription + "\" />");
			}

			// Execute hook if it's attached
			if (post != null && Hooks.Blog.UI.GetPostMeta != null)
				Hooks.Blog.UI.GetPostMeta(str);
			else if (archive != null && Hooks.Blog.UI.GetArchiveMeta != null)
				Hooks.Blog.UI.GetArchiveMeta(str);
		}

		#region Post Extensions
		/// <summary>
		/// Gets if the post has any categories attached to it.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>If the post has any categories.</returns>
		public static bool HasCategories(this Models.Post post) {
			return post.Categories.Count > 0;
		}

		/// <summary>
		/// Gets the categories formatted as a list of links.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <param name="delimeter">Optional delimeter</param>
		/// <returns>The rendered html</returns>
		public static IHtmlString GetCategories(this Models.Post post, string delimeter = ", ") {
			var sb = new StringBuilder();

			foreach (var cat in post.Categories) {
				if (sb.Length > 0)
					sb.Append(delimeter);
				if (Hooks.Blog.UI.GetPostCategory != null) {
					Hooks.Blog.UI.GetPostCategory(sb, cat);
				} else { 
					sb.Append(String.Format("<a href=\"{0}\">{1}</a>",
						Utils.Url("~/category/" + cat.Slug), cat.Name));
				}
			}
			return new HtmlString(sb.ToString());
		}

		/// <summary>
		/// Gets if the post has any tags attached to it.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>If the post has any tags.</returns>
		public static bool HasTags(this Models.Post post) {
			return post.Tags.Count > 0;
		}

		/// <summary>
		/// Gets the tags formatted as a list of links.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <param name="delimeter">Optional delimeter</param>
		/// <returns>The rendered html</returns>
		public static IHtmlString GetTags(this Models.Post post, string delimeter = ", ") {
			var sb = new StringBuilder();

			foreach (var tag in post.Tags) {
				if (sb.Length > 0)
					sb.Append(delimeter);
				if (Hooks.Blog.UI.GetPostTag != null) {
					Hooks.Blog.UI.GetPostTag(sb, tag);
				} else {
					sb.Append(String.Format("<a href=\"{0}\">{1}</a>",
						Utils.Url("~/category/" + tag.Slug), tag.Name));
				}
			}
			return new HtmlString(sb.ToString());
		}

		/// <summary>
		/// Gets the excerpt for the post.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>The post excerpt</returns>
		public static IHtmlString GetExcerpt(this Models.Post post) {
			if (Hooks.Blog.UI.GetPostExcerpt != null) {
				return new HtmlString(Hooks.Blog.UI.GetPostExcerpt(post));
			} else {
				Regex reg = new Regex("<p[^>]*>(?<text>.*?)</p>");
				var matches = reg.Matches(post.Html.ToHtmlString());

				if (matches.Count > 0)
					return new HtmlString(matches[0].Groups["text"].Value);
			}
			return new HtmlString("");
		}

		/// <summary>
		/// Gets the permalink for the post.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>The permalink</returns>
		public static string GetPermalink(this Models.Post post) {
			return Utils.Url("~/" + FormatPermalink(post));
		}

		/// <summary>
		/// Gets the absolute URL for the post.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>The url</returns>
		public static string GetAbsoluteUrl(this Models.Post post) {
			return Utils.AbsoluteUrl("~/" + FormatPermalink(post));
		}

		/// <summary>
		/// Gets the formatted published date.
		/// </summary>
		/// <param name="post">The current post</param>
		/// <returns>The published date</returns>
		public static string GetPublished(this Models.Post post) {
			if (Hooks.Blog.UI.GetPostPublished != null)
				return Hooks.Blog.UI.GetPostPublished(post);
			return post.Published.Value.ToString("yy MMM dd");
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Formats the permalink for the given post.
		/// </summary>
		/// <param name="post">The post</param>
		/// <returns>The formatted permalink</returns>
		private static string FormatPermalink(Models.Post post) {
			return post.Published.Value.Year + "/" +
				post.Published.Value.Month.ToString("00") + "/" +
				post.Published.Value.Day.ToString("00") + "/" +
				post.Slug;
		}
		#endregion
	}
}