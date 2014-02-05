/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.ComponentModel.Composition;
using Goldfish.Extend;

namespace Goldfish.Blocks
{
	/// <summary>
	/// Starts the blocks module.
	/// </summary>
	[Export(typeof(IModule))]
	public class BlocksModule : Module
	{
		/// <summary>
		/// Initializes the blocks module.
		/// </summary>
		public override void Init() {
			// Add cache for the blocks
			AddCache<Entities.Block>(b => b.Id, b => b.InternalId);
		}
	}
}