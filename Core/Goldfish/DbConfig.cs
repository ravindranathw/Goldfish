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