/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Runtime.Serialization;

namespace Goldfish.Models
{
	/// <summary>
	/// The standard archive model for the blog.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Archive : ArchiveBase
	{
		#region Properties
		/// <summary>
		/// Gets/sets the current archive year.
		/// </summary>
		[DataMember]
		public int? Year { get; set; }

		/// <summary>
		/// Gets/sets the current archive month.
		/// </summary>
		[DataMember]
		public int? Month { get; set; }
		#endregion
	}
}
