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
	/// The autor is used to describe users writing posts.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Author
	{
		#region Properties
		/// <summary>
		/// Gets the unique id.
		/// </summary>
		[DataMember]
		public Guid? Id { get; internal set; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the email adress.
		/// </summary>
		[DataMember]
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the biography.
		/// </summary>
		[DataMember]
		public string Bio { get; set; }
		#endregion
	}
}
