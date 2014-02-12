/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Goldfish.Helpers
{
	public static class UI
	{
		#region Members
		private static string ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
		#endregion

		/// <summary>
		/// Converts a virtual (relative) path to a theme url.
		/// </summary>
		/// <param name="virtualPath">The virtual path</param>
		/// <returns>The theme path</returns>
		public static string Url(string virtualPath) {
			return Utils.AbsoluteUrl(virtualPath.Replace("~/", "~/themes/" + Config.App.Theme + "/"));
		}

		/// <summary>
		/// Converts a virtual (relative) path to a theme absolute path.
		/// </summary>
		/// <param name="virtualPath">The virtual path</param>
		/// <returns>The theme path</returns>
		public static string Path(string virtualPath) {
			return virtualPath.Replace("~/", "~/themes/" + Config.App.Theme + "/");
		}

		/// <summary>
		/// Gets the meta tags for the current request.
		/// </summary>
		/// <returns>The meta tags</returns>
		public static IHtmlString Meta() {
			var str = new StringBuilder();

			// Generator
			str.AppendLine("<meta name=\"generator\" content=\"Goldfish " + ProductVersion + "\" />");

			// Execute hooks
			if (Hooks.App.UI.GetMeta != null)
				Hooks.App.UI.GetMeta(str);

			return new HtmlString(str.ToString());
		}

		/// <summary>
		/// Gets the requested scripts for the current request.
		/// </summary>
		/// <returns>The scripts</returns>
		public static IHtmlString Scripts() {
			var str = new StringBuilder();

			// Execute hooks
			if (Hooks.App.UI.GetScripts != null)
				Hooks.App.UI.GetScripts(str);

			return new HtmlString(str.ToString());
		}

		/// <summary>
		/// Generates a gravatar url for the given email and size.
		/// </summary>
		/// <param name="email">The email address</param>
		/// <param name="size">The size in pixels</param>
		/// <returns>The gravatar url</returns>
		public static string GravatarUrl(string email, int size) {
			var md5 = new MD5CryptoServiceProvider();

			var encoder = new UTF8Encoding();
			var hash = new MD5CryptoServiceProvider();
			var bytes = hash.ComputeHash(encoder.GetBytes(email));

			var sb = new StringBuilder(bytes.Length * 2);
			for (int n = 0; n < bytes.Length; n++) {
				sb.Append(bytes[n].ToString("X2"));
			}

			return "http://www.gravatar.com/avatar/" + sb.ToString().ToLower() +
				(size > 0 ? "?s=" + size : "");
		}

		#region Extensions
		/// <summary>
		/// Gets the formatted date.
		/// </summary>
		/// <param name="post">The date</param>
		/// <returns>The formatted date</returns>
		public static string Format(this DateTime? date) {
			if (date.HasValue)
				return Format(date.Value);
			return "";
		}

		/// <summary>
		/// Gets the formatted date.
		/// </summary>
		/// <param name="post">The date</param>
		/// <returns>The formatted date</returns>
		public static string Format(this DateTime date) {
			if (Hooks.App.UI.FormatDate != null)
				return Hooks.App.UI.FormatDate(date);
			return date.ToString("yy MMM dd");
		}
		#endregion
	}
}