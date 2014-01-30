/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace Goldfish
{
	/// <summary>
	/// The main low level data context.
	/// </summary>
	public class Db : IdentityDbContext<Entities.User>
	{
		#region Db sets
		/// <summary>
		/// Gets/sets the available authors.
		/// </summary>
		public DbSet<Entities.Author> Authors { get; set; }

		/// <summary>
		/// Gets/sets the available categories.
		/// </summary>
		public DbSet<Entities.Category> Categories { get; set; }

		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		public DbSet<Entities.Comment> Comments { get; set; }

		/// <summary>
		/// Gets/sets the available parameters.
		/// </summary>
		public DbSet<Entities.Param> Params { get; set; }

		/// <summary>
		/// Gets/sets the available posts.
		/// </summary>
		public DbSet<Entities.Post> Posts { get; set; }

		/// <summary>
		/// Gets/sets the available tags.
		/// </summary>
		public DbSet<Entities.Tag> Tags { get; set; }
		#endregion

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="nameOrConnectionString">The name of, or the connection string</param>
		internal Db(string nameOrConnectionString = "goldfish")
			: base(nameOrConnectionString) {
			// Set the context initializer
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Migrations.Configuration>());

			// Attach OnModelCreating event
			((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
				new ObjectMaterializedEventHandler(OnEntityLoad);
		}

		/// <summary>
		/// Creates and initializes the data context.
		/// </summary>
		/// <param name="modelBuilder">The current model builder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new Entities.Maps.AuthorMap());
			modelBuilder.Configurations.Add(new Entities.Maps.CategoryMap());
			modelBuilder.Configurations.Add(new Entities.Maps.CommentMap());
			modelBuilder.Configurations.Add(new Entities.Maps.ParamMap());
			modelBuilder.Configurations.Add(new Entities.Maps.PostMap());
			modelBuilder.Configurations.Add(new Entities.Maps.TagMap());
			modelBuilder.Configurations.Add(new Entities.Maps.UserMap());

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Called when an entity has been loaded.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">Event arguments</param>
		private void OnEntityLoad(object sender, ObjectMaterializedEventArgs e) {
			if (e.Entity is Entities.IBaseEntity)
				((Entities.IBaseEntity)e.Entity).OnLoad(this);
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of changes</returns>
		public override int SaveChanges() {
			var cached = new List<Cache.ICacheEntity>();

			foreach (var entry in ChangeTracker.Entries()) {
				// Call the correct entity event.
				if (entry.Entity is Entities.IBaseEntity) {
					if (entry.State == EntityState.Added || entry.State == EntityState.Modified) {
						((Entities.IBaseEntity)entry.Entity).OnSave(this, entry.State);
					} else if (entry.State == EntityState.Deleted) {
						((Entities.IBaseEntity)entry.Entity).OnDelete(this);
					}
				}
				// Check if entity is cached
				if (entry.Entity is Cache.ICacheEntity)
					cached.Add((Cache.ICacheEntity)entry.Entity);
			}
			// Save the changes
			var ret = base.SaveChanges();

			// Remove all cached entities from cache
			foreach (var entity in cached)
				entity.RemoveFromCache();

			// Return the number of saved changes
			return ret;
		}
	}
}
