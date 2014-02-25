/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Threading.Tasks;
using AutoMapper;
using Goldfish.Helpers;

namespace Goldfish.Pages
{
	/// <summary>
	/// The pages repository.
	/// </summary>
	public class Api : IDisposable
	{
		#region Members
		private Db db;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the page repository.
		/// </summary>
		public Repositories.PageRepository Pages { get; private set; }

		/// <summary>
		/// Gets the navigation repository.
		/// </summary>
		public Repositories.NavRepository Navigation { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Api() {
			db = new Db();

			Pages = new Repositories.PageRepository(db);
			Navigation = new Repositories.NavRepository(db);
		}

		/// <summary>
		/// Saves the changes made to the repository.
		/// </summary>
		/// <returns>The number of changes saved</returns>
		public int SaveChanges() {
			return db.SaveChanges();
		}

		/// <summary>
		/// Disposes the repository.
		/// </summary>
		public void Dispose() {
			db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}