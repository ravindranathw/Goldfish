/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;

namespace Goldfish
{
	/// <summary>
	/// Db context configuration.
	/// </summary>
	internal class DbConfig : DbConfiguration
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DbConfig() {
			if (Hooks.App.Db.Configure != null)
				Hooks.App.Db.Configure(this);
		}
	}
}