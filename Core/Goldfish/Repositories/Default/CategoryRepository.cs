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
using System.Threading.Tasks;

using AutoMapper;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Default implementation of the category repository.
	/// </summary>
	internal class CategoryRepository : SlugRepository<Models.Category, Entities.Category>, ICategoryRepository
	{ 
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		public CategoryRepository(Db db)
			: base(db, App.Instance.ModelCache.Categories, Hooks.Blog.Model.OnCategoryLoad) { }

		/// <summary>
		/// Gets the entity query for the given key.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Category, bool>> GetByKey(string key) {
			return c => c.Slug == key;
		}

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Category, bool>> Get(Models.Category model) {
			return c => c.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Category, bool>> Get(Guid id) {
			return c => c.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Category> GetQuery(Expression<Func<Entities.Category, bool>> predicate = null) {
			if (predicate != null)
				return uow.Categories.Where(predicate).OrderBy(c => c.Name);
			else return uow.Categories.OrderBy(c => c.Name);

		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Category entity) {
			uow.Categories.Remove(entity);
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Category source, Entities.Category dest) {
			var state = new Models.ModelState();

			// Ensure slug format
			if (String.IsNullOrEmpty(source.Slug))
				source.Slug = Utils.GenerateSlug(source.Name);
			else source.Slug = Utils.GenerateSlug(source.Slug);

			// Execute hooks
			if (Hooks.Blog.Model.OnCategorySave!= null)
				Hooks.Blog.Model.OnCategorySave(source, state);

			// Proceed if state is valid
			if (state.IsValid) {
				if (dest == null) { 
					dest = new Entities.Category();
					source.Id = dest.Id = Guid.NewGuid();

					uow.Categories.Add(dest);
				}
				Mapper.Map<Models.Category, Entities.Category>(source, dest);
			} else { 
				throw new Models.ModelStateException("Error while adding category. See data for details", state);
			}
		}
	}
}
