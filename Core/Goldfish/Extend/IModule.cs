/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish.Extend
{
	/// <summary>
	/// Interface for a module.
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		void Init();
	}
}
