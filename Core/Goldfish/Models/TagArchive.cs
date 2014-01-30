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
	/// Archive model filtered on tag.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class TagArchive : ArchiveBase
	{
		#region Properties
		/// <summary>
		/// Gets/sets the selected tag.
		/// </summary>
		[DataMember]
		public Tag Tag { get; internal set; }
		#endregion
	}
}
