/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish.Blocks.Entities
{
	/// <summary>
	/// A block can be used to store a piece of content to be
	/// used in the block.
	/// </summary>
	[Serializable]
	public sealed class Block
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the unique internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the body.
		/// </summary>
		public string Body { get; set; }
		#endregion
	}
}