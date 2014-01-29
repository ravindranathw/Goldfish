using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Goldfish.Web
{
	/// <summary>
	/// Base class for handling embedded resources.
	/// </summary>
	public abstract class ResourceManager : IDisposable
	{
		#region Members
		/// <summary>
		/// The private collection of embedded resources.
		/// </summary>
		private static Dictionary<string, Resource> Resources;

		/// <summary>
		/// The manager assembly.
		/// </summary>
		internal Assembly Assembly;

		/// <summary>
		/// The last write date of the manager assembly.
		/// </summary>
		private DateTime LastMod;

		/// <summary>
		/// Mutex for initialization.
		/// </summary>
		private object mutex = new object();
		#endregion

		/// <summary>
		/// Private constructor.
		/// </summary>
		protected ResourceManager(Assembly assembly) {
			Assembly = assembly;
			LastMod = new FileInfo(assembly.Location).LastWriteTime;

			if (Resources == null) {
				lock (mutex) {
					if (Resources == null) { 
						var res = new Dictionary<string, Resource>();

						foreach (var name in assembly.GetManifestResourceNames()) {
							res.Add(name.Replace(GetAssemblyName(assembly), "").ToLower(), new Resource(this) {
								Name = name, MimeType = GetMimeType(name)
							});
						}
						Resources = res;
					}
				}
			}
		}

		/// <summary>
		/// Gets the resource with the given path.
		/// </summary>
		/// <param name="path">The path</param>
		/// <returns>The resource, null if it wasn't found</returns>
		public virtual Resource Get(string path) {
			path = path.Replace("/", ".");

			if (Resources.ContainsKey(path))
				return Resources[path];
			return null;
		}

		/// <summary>
		/// Gets the last modification date for the assembly.
		/// </summary>
		/// <returns>The date</returns>
		public virtual DateTime GetLastMod() {
			return LastMod;
		}

		/// <summary>
		/// Disposes the manager.
		/// </summary>
		public void Dispose() {
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the mime type from the resource name.
		/// </summary>
		/// <param name="name">The resource name</param>
		/// <returns>The content type</returns>
		protected virtual string GetMimeType(string name) {
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

		#region Private methods
		/// <summary>
		/// Gets the assembly name.
		/// </summary>
		/// <param name="assembly">The assembly</param>
		/// <returns>The name</returns>
		private string GetAssemblyName(Assembly assembly) {
			return assembly.FullName.Substring(0, assembly.FullName.IndexOf(",")) + ".";
		}
		#endregion
	}
}