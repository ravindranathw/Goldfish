using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Goldfish.Models
{
	/// <summary>
	/// Base class for the archive models.
	/// </summary>
	[Serializable]
	[DataContract]
	public class ArchiveBase
	{
		#region Properties
		/// <summary>
		/// Gets/sets the currently available posts.
		/// </summary>
		[DataMember]
		public IList<Post> Posts { get; internal set; }

		/// <summary>
		/// Gets/sets the current page.
		/// </summary>
		[DataMember]
		public int CurrentPage { get; internal set; }

		/// <summary>
		/// Gets/sets the number of available pages for the
		/// current archive.
		/// </summary>
		[DataMember]
		public int PageCount { get; internal set; }

		/// <summary>
		/// Gets/sets the requested page size.
		/// </summary>
		[DataMember]
		public int PageSize { get; internal set; }

		/// <summary>
		/// Gets/sets the total amount of posts for the archive.
		/// </summary>
		[DataMember]
		public int TotalCount { get; internal set; }
		#endregion
	}
}
