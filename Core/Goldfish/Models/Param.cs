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
	/// Params are used to store application settings that should be
	/// manageble by the content administrators.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Param
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		[DataMember]
		public Guid? Id { get; internal set; }

		/// <summary>
		/// Gets/sets the unique internal id.
		/// </summary>
		[DataMember]
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the value.
		/// </summary>
		[DataMember]
		public string Value { get; set; }
		#endregion
	}
}
