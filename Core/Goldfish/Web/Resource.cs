using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Web
{
	/// <summary>
	/// Class defining an embedded resource.
	/// </summary>
	public sealed class Resource
	{
		#region Properties
		/// <summary>
		/// Gets/sets the assembly resource name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the mime type of the resource.
		/// </summary>
		public string MimeType { get; set; }

		/// <summary>
		/// Gets/the resource manager this resource belongs to.
		/// </summary>
		private ResourceManager Manager { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The manager this resource belongs to</param>
		public Resource(ResourceManager manager) {
			Manager = manager;
		}

		/// <summary>
		/// Gets the embedded content for the assets.
		/// </summary>
		/// <returns></returns>
		public byte[] GetData() {
			using (var stream = Manager.Assembly.GetManifestResourceStream(Name)) {
				var bytes = new byte[stream.Length];
				stream.Read(bytes, 0, Convert.ToInt32(stream.Length));

				return bytes;
			}
		}
	}
}