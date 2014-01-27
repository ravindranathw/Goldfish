using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Goldfish.Manager
{
	/// <summary>
	/// Internal class for handling embedded assets.
	/// </summary>
	internal sealed class AssetsManager
	{
		#region Inner classes
		/// <summary>
		/// Class defining a asset.
		/// </summary>
		internal class Asset
		{
			/// <summary>
			/// Gets/sets the assembly asset name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the mime type of the asset.
			/// </summary>
			public string MimeType { get; set; }

			/// <summary>
			/// Gets the embedded content for the assets.
			/// </summary>
			/// <returns></returns>
			public byte[] GetData() {
				using (var stream = AssetsManager.Instance.assembly.GetManifestResourceStream(Name)) {
					var bytes = new byte[stream.Length];
					stream.Read(bytes, 0, Convert.ToInt32(stream.Length));

					return bytes;
				}
			}
		}
		#endregion

		#region Members
		/// <summary>
		/// The private collection of embedded assets.
		/// </summary>
		private Dictionary<string, Asset> assets;

		/// <summary>
		/// The manager assembly.
		/// </summary>
		private Assembly assembly;

		/// <summary>
		/// The last write date of the manager assembly.
		/// </summary>
		private DateTime lastmod;

		/// <summary>
		/// The public singleton instance of the resource manager.
		/// </summary>
		public static readonly AssetsManager Instance = new AssetsManager();
		#endregion

		/// <summary>
		/// Private constructor.
		/// </summary>
		private AssetsManager() {
			assembly = typeof(AssetsManager).Assembly;
			lastmod = new FileInfo(assembly.Location).LastWriteTime;
			assets = new Dictionary<string, Asset>();

			foreach (var name in assembly.GetManifestResourceNames()) {
				assets.Add(name.Replace("Goldfish.Areas.Manager.Assets.", "").ToLower(), new Asset() {
					Name = name, MimeType = GetMimeType(name)
				});
			}
		}

		/// <summary>
		/// Gets the asset with the given path.
		/// </summary>
		/// <param name="path">The path</param>
		/// <returns>The resource, null if it wasn't found</returns>
		internal Asset Get(string path) {
			path = path.Replace("/", ".");

			if (assets.ContainsKey(path))
				return assets[path];
			return null;
		}

		/// <summary>
		/// Gets the last modification date for the assembly.
		/// </summary>
		/// <returns>The date</returns>
		internal DateTime GetLastMod() {
			return lastmod;
		}

		#region Private methods
		/// <summary>
		/// Gets the mime type from the resource name.
		/// </summary>
		/// <param name="name">The resource name</param>
		/// <returns>The content type</returns>
		private string GetMimeType(string name) {
			if (name.EndsWith(".js")) {
				return "text/javascript";
			} else if (name.EndsWith(".css")) {
				return "text/css";
			} else if (name.EndsWith(".png")) {
				return "image/png";
			} else if (name.EndsWith(".jpg")) {
				return "image/jpg";
			} else if (name.EndsWith(".gif")) {
				return "image/gif";
			} else if (name.EndsWith(".ico")) {
				return "image/ico";
			} else if (name.EndsWith(".eot")) {
				return "application/vnd.ms-fontobject";
			} else if (name.EndsWith(".ttf")) {
				return "application/octet-stream";
			} else if (name.EndsWith(".svg")) {
				return "image/svg+xml";
			} else if (name.EndsWith(".woff")) {
				return "application/x-woff";
			}
			return "application/unknown";
		}
		#endregion
	}
}