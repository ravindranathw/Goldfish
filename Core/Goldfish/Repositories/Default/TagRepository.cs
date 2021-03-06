﻿/*
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
	/// Default implementation of the tag repository.
	/// </summary>
	internal class TagRepository : SlugRepository<Models.Tag, Entities.Tag>, ITagRepository
	{ 
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		public TagRepository(Db db)
			: base(db, App.Instance.ModelCache.Tags, Hooks.Blog.Model.Tag.OnLoad) { }

		/// <summary>
		/// Gets the entity query for the given key.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Tag, bool>> GetByKey(string key) {
			return t => t.Slug == key;
		}

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Tag, bool>> Get(Models.Tag model) {
			return t => t.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Tag, bool>> Get(Guid id) {
			return t => t.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Tag> GetQuery(Expression<Func<Entities.Tag, bool>> predicate = null) {
			if (predicate != null)
				return uow.Tags.Where(predicate).OrderBy(t => t.Name);
			else return uow.Tags.OrderBy(t => t.Name);

		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Tag entity) {
			uow.Tags.Remove(entity);
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Tag source, Entities.Tag dest) {
			var state = new Models.ModelState();

			// Ensure slug format
			if (String.IsNullOrEmpty(source.Slug))
				source.Slug = Utils.GenerateSlug(source.Name);
			else source.Slug = Utils.GenerateSlug(source.Slug);

			// Execute hooks
			if (Hooks.Blog.Model.Tag.OnSave != null)
				Hooks.Blog.Model.Tag.OnSave(source, state);

			// Proceed if state is valid
			if (state.IsValid) {
				if (dest == null) { 
					dest = new Entities.Tag();
					source.Id = dest.Id = Guid.NewGuid();

					uow.Tags.Add(dest);
				}
				Mapper.Map<Models.Tag, Entities.Tag>(source, dest);
			} else { 
				throw new Models.ModelStateException("Error while adding tag. See data for details", state);
			}
		}
	}
}
