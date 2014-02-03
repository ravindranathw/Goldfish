/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Default implementation of the param repository.
	/// </summary>
	internal class ParamRepository : InternalIdRepository<Models.Param, Entities.Param>, IParamRepository
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current db context</param>
		public ParamRepository(Db db)
			: base(db, App.Instance.ModelCache.Params, Hooks.App.Model.Param.OnLoad) { }

		/// <summary>
		/// Gets the entity query for the given key.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Param, bool>> GetByKey(string key) {
			return p => p.InternalId == key;
		}

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Param, bool>> Get(Models.Param model) {
			return p => p.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Param, bool>> Get(Guid id) {
			return p => p.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Param> GetQuery(Expression<Func<Entities.Param, bool>> predicate = null) {
			if (predicate != null)
				return uow.Params.Where(predicate).OrderBy(p => p.Name);
			return uow.Params.OrderBy(p => p.Name);
		}


		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Param entity) {
			if (!entity.IsSystemParam)
				uow.Params.Remove(entity);
			else throw new UnauthorizedAccessException("System parameters can't be removed.");
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Param source, Entities.Param dest) {
			var state = new Models.ModelState();

			// Execute hooks
			if (Hooks.App.Model.Param.OnSave != null)
				Hooks.App.Model.Param.OnSave(source, state);

			// Proceed if state is valid
			if (state.IsValid) {
				if (dest == null) {
					dest = new Entities.Param();
					source.Id = dest.Id = Guid.NewGuid();

					uow.Params.Add(dest);
				}
				Mapper.Map<Models.Param, Entities.Param>(source, dest);
			} else { 
				throw new Models.ModelStateException("Error while adding param. See data for details", state);
			}
		}
	}
}
