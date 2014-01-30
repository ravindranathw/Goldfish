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
	/// Categories are used to group posts together.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Category
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		[DataMember]
		public Guid? Id { get; internal set; }

		/// <summary>
		/// Gets/sets the unique slug for the category.
		/// </summary>
		[DataMember]
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[DataMember]
		public string Name { get; set; }
		#endregion
	}
}
