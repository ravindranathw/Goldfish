using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Abstract base class for internal id repositories.
	/// </summary>
	/// <typeparam name="TModel">The model type</typeparam>
	/// <typeparam name="TEntity">The entity type</typeparam>
	internal abstract class InternalIdRepository<TModel, TEntity> : BaseRepository<TModel, TEntity>
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current data context.</param>
		/// <param name="cache"></param>
		/// <param name="hook"></param>
		public InternalIdRepository(Db db, Cache.ModelCache<TModel> cache, Hooks.Delegates.ModelDelegate<TModel> hook)
			: base(db, cache, hook) {}

		/// <summary>
		/// Gets the model with the given slug. 
		/// </summary>
		/// <param name="internalId">The unique internal id</param>
		/// <returns>The model</returns>
		public virtual TModel GetByInternalId(string internalId) { 
			internalId = internalId.ToUpper();

			var model = Cache != null ? Cache.Get(internalId) : default(TModel);

			if (model == null)
				model = Get(GetByKey(internalId)).SingleOrDefault();
			return model;
		}

		/// <summary>
		/// Gets the model with the given slug. 
		/// </summary>
		/// <param name="internalId">The unique internal id</param>
		/// <returns>The model</returns>
		public virtual async Task<TModel> GetByInternalIdAsync(string internalId) { 
			internalId = internalId.ToUpper();

			var model = Cache != null ? Cache.Get(internalId) : default(TModel);

			if (model == null)
				model = (await GetAsync(GetByKey(internalId))).SingleOrDefault();
			return model;
		}

		#region Abstract methods
		/// <summary>
		/// Gets the entity query for the given slug.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected abstract Expression<Func<TEntity, bool>> GetByKey(string key);
		#endregion
	}
}