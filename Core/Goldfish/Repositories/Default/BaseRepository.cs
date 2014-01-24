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
	/// Abstract base class for repositories.
	/// </summary>
	/// <typeparam name="TModel">The model type</typeparam>
	/// <typeparam name="TEntity">The entity type</typeparam>
	internal abstract class BaseRepository<TModel, TEntity>
	{
		#region Members
		protected readonly Cache.ModelCache<TModel> Cache;
		protected readonly Hooks.Delegates.ModelDelegate<TModel> Hook;
		protected readonly Db uow;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current data context.</param>
		/// <param name="cache">The optional cache</param>
		/// <param name="hook">The optional hook</param>
		public BaseRepository(Db db, Cache.ModelCache<TModel> cache, Hooks.Delegates.ModelDelegate<TModel> hook) {
			Cache = cache;
			Hook = hook;
			uow = db;
		}

		/// <summary>
		/// Gets the model with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public virtual TModel GetById(Guid id) { 
			var model = Cache != null ? Cache.Get(id) : default(TModel);

			if (model == null)
				model = Get(Get(id)).SingleOrDefault();
			if (model != null)
				Process(model);
			return model;
		}

		/// <summary>
		/// Gets the model with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public virtual async Task<TModel> GetByIdAsync(Guid id) { 
			var model = Cache != null ? Cache.Get(id) : default(TModel);

			if (model == null)
				model = (await GetAsync(Get(id))).SingleOrDefault();
			if (model != null)
				Process(model);
			return model;
		}

		/// <summary>
		/// Gets the categories matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The categories</returns>
		public virtual IList<TModel> Get(Expression<Func<TEntity, bool>> predicate = null) { 
			var models = new List<TModel>();
			var entities = GetQuery(predicate).ToList();

			foreach (var e in entities) {
				var model = Map(e);

				// Execute hook if registered
				if (Hook != null)
					Hook(model);

				if (Cache != null)
					Cache.Add(model);
				Process(model);

				models.Add(model);
			}
			return models;
		}

		/// <summary>
		/// Gets the categories matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The categories</returns>
		public virtual async Task<IList<TModel>> GetAsync(Expression<Func<TEntity, bool>> predicate = null) { 
			var models = new List<TModel>();
			var entities = await GetQuery(predicate).ToListAsync();

			foreach (var e in entities) {
				var model = Map(e);

				// Execute hook if registered
				if (Hook != null)
					Hook(model);

				if (Cache != null)
					Cache.Add(model);
				Process(model);

				models.Add(model);
			}
			return models;
		}

		/// <summary>
		/// Adds the given model.
		/// </summary>
		/// <param name="model">The model</param>
		public virtual void Add(TModel model) { 
			var dest = GetQuery(Get(model))
				.SingleOrDefault();
			Add(model, dest);
		}

		/// <summary>
		/// Removes the given model.
		/// </summary>
		/// <param name="model">The model</param>
		public virtual void Remove(TModel model) { 
			var entity = GetQuery(Get(model)).Single();
			Remove(entity);
		}

		/// <summary>
		/// Removes the model with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public virtual void Remove(Guid id) { 
			var entity = GetQuery(Get(id)).Single();
			Remove(entity);			
		}

		/// <summary>
		/// Maps the given entity to a model.
		/// </summary>
		/// <param name="entity">The entity</param>
		/// <returns>The mapped model</returns>
		protected virtual TModel Map(TEntity entity) {
			return Mapper.Map<TEntity, TModel>(entity);
		}

		/// <summary>
		/// Performs any additional processing after the model has been fetched
		/// and added to cache.
		/// </summary>
		/// <param name="model">The model</param>
		protected virtual void Process(TModel model) {}

		#region Abstract methods
		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected abstract Expression<Func<TEntity, bool>> Get(TModel model);

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected abstract Expression<Func<TEntity, bool>> Get(Guid id);

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected abstract IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null);

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected abstract void Remove(TEntity entity);

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected abstract void Add(TModel source, TEntity dest);
		#endregion
	}
}