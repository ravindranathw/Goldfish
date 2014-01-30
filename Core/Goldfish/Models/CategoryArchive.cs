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
	/// Archive model filtered on category.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class CategoryArchive : ArchiveBase
	{
		#region Properties
		/// <summary>
		/// Gets/sets the selected category.
		/// </summary>
		[DataMember]
		public Category Category { get; internal set; }
		#endregion
	}
}
