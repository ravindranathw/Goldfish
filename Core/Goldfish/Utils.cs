/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using MarkdownSharp;

namespace Goldfish
{
	/// <summary>
	/// Internal static utility class with global helper methods.
	/// </summary>
	public static class Utils
	{
		#region Properties
		/// <summary>
		/// Gets the currently requested model in the current http context.
		/// </summary>
		internal static object Current {
			get { return HttpContext.Current.Items["Goldfish_Current"]; }
			set { HttpContext.Current.Items["Goldfish_Current"] = value; }
		}
		#endregion

		#region Utility methods
		/// <summary>
		/// Generates an absolute url from the given virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>The absolute url</returns>
		public static string AbsoluteUrl(string virtualpath) {
			var request = HttpContext.Current.Request;

			// First, convert virtual paths to site url's
			if (virtualpath.StartsWith("~/"))
				virtualpath = Url(virtualpath);

			// Now add server, scheme and port
			return request.Url.Scheme + "://" + request.Url.DnsSafeHost +
				(!request.Url.IsDefaultPort ? ":" + request.Url.Port.ToString() : "") + virtualpath;
		}

		/// <summary>
		/// Generates an url from the given virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>The url</returns>
		public static string Url(string virtualpath) {
			var request = HttpContext.Current.Request;
			return virtualpath.Replace("~/", request.ApplicationPath + (request.ApplicationPath != "/" ? "/" : ""));
		}

		/// <summary>
		/// Generates a slug from the given string.
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>The slug</returns>
		public static string GenerateSlug(string str) {
			var slug = Regex.Replace(str.ToLower()
				.Replace(" ", "-")
				.Replace("å", "a")
				.Replace("ä", "a")
				.Replace("á", "a")
				.Replace("à", "a")
				.Replace("ö", "o")
				.Replace("ó", "o")
				.Replace("ò", "o")
				.Replace("é", "e")
				.Replace("è", "e")
				.Replace("í", "i")
				.Replace("ì", "i"), @"[^a-z0-9-/]", "").Replace("--", "-");

			if (slug.EndsWith("-"))
				slug = slug.Substring(0, slug.LastIndexOf("-"));
			if (slug.StartsWith("-"))
				slug = slug.Substring(Math.Min(slug.IndexOf("-") + 1, slug.Length));
			return slug;
		}

		/// <summary>
		/// Generates a slug for the given filename.
		/// </summary>
		/// <param name="str">The filename</param>
		/// <returns>The slug</returns>
		public static string GenerateFileSlug(string filename) {
			var index = filename.LastIndexOf('.');

			if (index != -1) {
				var name = filename.Substring(0, index);
				var ending = filename.Substring(index);

				return GenerateSlug(name) + ending;
			}
			return GenerateSlug(filename);
		}

		/// <summary>
		/// Gets the param value with the given key.
		/// </summary>
		/// <typeparam name="T">The value type</typeparam>
		/// <param name="key">The internal id</param>
		/// <param name="func">The conversion function</param>
		/// <returns>The param value</returns>
		public static T GetParam<T>(string key, Func<string, T> func) {
			using (var api = App.Instance.IoCContainer.Resolve<IApi>()) {
				return func(api.Params.GetByInternalId(key).Value);
			}
		}

		/// <summary>
		/// Sets tbe param value with the given key.
		/// </summary>
		/// <param name="key">The internal id</param>
		/// <param name="value">The value</param>
		public static void SetParam(string key, object value) {
			using (var api = App.Instance.IoCContainer.Resolve<IApi>()) { 
				var param = api.Params.GetByInternalId(key);

				param.Value = value.ToString();
				api.Params.Add(param);

				api.SaveChanges();				
			}
		}

		/// <summary>
		/// Transforms the given markdown string to html.
		/// </summary>
		/// <param name="str">The markdown string</param>
		/// <returns></returns>
		public static string TransformMarkdown(string str) {
			var converter = new Markdown();
			return converter.Transform(str);
		}
		#endregion

		#region Extension methods
		/// <summary>
		/// Gets a subset of the current enumerable list.
		/// </summary>
		/// <typeparam name="T">The list type</typeparam>
		/// <param name="set">The enumerable set</param>
		/// <param name="startpos">From what position</param>
		/// <param name="length">How many items</param>
		/// <returns>The subset</returns>
		internal static IEnumerable<T> Subset<T>(this IEnumerable<T> set, int startpos = 0, int length = 0) {
			List<T> subset = new List<T>();
			var pos = 0;

			length = length > 0 ? length : Int32.MaxValue - pos;

			foreach (var item in set) {
				if (pos >= startpos && pos < (startpos + length))
					subset.Add(item);
				if (pos++ >= (startpos + length))
					break;
			}
			return subset;
		}

		/// <summary>
		/// Generates nofollow html links for all urls in the given string.
		/// </summary>
		/// <param name="str">The text</param>
		/// <returns>The processed text</returns>
		public static string GenerateLinks(this string str) {
			var regx = new Regex(@"http://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\'‌​\,]*)?");
			var matches = regx.Matches(str);

			foreach (Match match in matches) { 
				str = str.Replace(match.Value, String.Format("<a href=\"{0}\" rel=\"nofollow\">{0}</a>", match.Value));
			}
			return str;

		}

		/// <summary>
		/// Removes all html tags from the given string.
		/// </summary>
		/// <param name="str">The text</param>
		/// <returns>The processed text</returns>
		public static string StripHtml(this string str) {
			return Regex.Replace(str, "<.*?>", "");
		}
		#endregion
	}
}
