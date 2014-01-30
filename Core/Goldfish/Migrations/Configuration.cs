/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Goldfish.Migrations
{
	/// <summary>
	/// Migrations configuration.
	/// </summary>
	internal sealed class Configuration : DbMigrationsConfiguration<Goldfish.Db>
    {
		/// <summary>
		/// Default constructor.
		/// </summary>
        public Configuration() {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Goldfish.Db";
        }

		/// <summary>
		/// Seed the database.
		/// </summary>
		/// <param name="db">The context</param>
        protected override void Seed(Db db) {
			// Default admin role
			var role = db.Roles.Where(r => r.Name == "Admin").SingleOrDefault();
			if (role == null) {
				role = new IdentityRole() { 
					Name = "Admin"
				};
				db.Roles.Add(role);
			}

			// Default params
			var param = db.Params.Where(p => p.InternalId == "ARCHIVE_PAGE_SIZE").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "ARCHIVE_PAGE_SIZE",
					Name = "Archive page size",
					IsSystemParam = true,
					Value = "5"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "BLOG_TITLE").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() {
					InternalId = "BLOG_TITLE",
					Name = "Blog title",
					IsSystemParam = true,
					Value = "Goldfish"
				};
				db.Params.Add(param);
			}
	
			param = db.Params.Where(p => p.InternalId == "BLOG_DESCRIPTION").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "BLOG_DESCRIPTION",
					Name = "Blog description",
					IsSystemParam = true,
					Value = "The lightweight & unobtrusive blogging platform for .NET"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "BLOG_META_KEYWORDS").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "BLOG_META_KEYWORDS",
					Name = "Blog meta keywords",
					IsSystemParam = true,
					Value = "Blog, Goldfish, lightweight, unobtrusive, .NET"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "BLOG_META_DESCRIPTION").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "BLOG_META_DESCRIPTION",
					Name = "Blog meta description",
					IsSystemParam = true,
					Value = "The lightweight & unobtrusive blogging platform for .NET"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "THEME").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "THEME",
					Name = "Theme",
					IsSystemParam = true,
					Value = "Casper"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "CACHE_ENABLED").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "CACHE_ENABLED",
					Name = "Cache enabled",
					IsSystemParam = true,
					Value = true.ToString()
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "CACHE_EXPIRES").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "CACHE_EXPIRES",
					Name = "Cache expiration time",
					IsSystemParam = true,
					Value = "30"
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "COMMENT_TYPE").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "COMMENT_TYPE",
					Name = "Active comment type",
					IsSystemParam = true,
					Value = Models.CommentType.Enabled.ToString()
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "COMMENT_MODERATE_AUTHORIZED").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "COMMENT_MODERATE_AUTHORIZED",
					Name = "Moderate comments from authorized users",
					IsSystemParam = true,
					Value = false.ToString()
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "COMMENT_MODERATE_ANONYMOUS").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "COMMENT_MODERATE_ANONYMOUS",
					Name = "Moderate comments from anonymous users",
					IsSystemParam = true,
					Value = false.ToString()
				};
				db.Params.Add(param);
			}

			param = db.Params.Where(p => p.InternalId == "COMMENT_DISQUS_ID").SingleOrDefault();
			if (param == null) {
				param = new Entities.Param() { 
					InternalId = "COMMENT_DISQUS_ID",
					Name = "External Disqus id",
					IsSystemParam = true,
					Value = ""
				};
				db.Params.Add(param);
			}

			// Save changes
			db.SaveChanges();

			// Seed application if the hook is attached
			if (Hooks.App.Db.Seed != null)
				Hooks.App.Db.Seed(db);
        }
    }
}
