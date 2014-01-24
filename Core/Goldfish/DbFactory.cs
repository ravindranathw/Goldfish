using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Goldfish
{
	/// <summary>
	/// Factory for creating a new Db.
	/// </summary>
	public class DbFactory : IDbContextFactory<Db>
	{
		/// <summary>
		/// Creates a new Db context.
		/// </summary>
		/// <returns>The context</returns>
		public Db Create() {
			return new Db();
		}
	}
}