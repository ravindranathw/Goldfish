using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Abstract base class for slug repositories.
	/// </summary>
	/// <typeparam name="TModel">The model type</typeparam>
	/// <typeparam name="TEntity">The entity type</typeparam>
	internal abstract class SlugRepository<TModel, TEntity> : BaseRepository<TModel, TEntity>
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current data context.</param>
		/// <param name="cache"></param>
		/// <param name="hook"></param>
		public SlugRepository(Db db, Cache.ModelCache<TModel> cache, Hooks.Delegates.ModelDelegate<TModel> hook)
			: base(db, cache, hook) {}

		/// <summary>
		/// Gets the model with the given slug. 
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public virtual TModel GetBySlug(string slug) { 
			var model = Cache != null ? Cache.Get(slug) : default(TModel);

			if (model == null)
				model = Get(GetByKey(slug)).SingleOrDefault();
			return model;
		}

		/// <summary>
		/// Gets the model with the given slug. 
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public virtual async Task<TModel> GetBySlugAsync(string slug) { 
			var model = Cache != null ? Cache.Get(slug) : default(TModel);

			if (model == null)
				model = (await GetAsync(GetByKey(slug))).SingleOrDefault();
			return model;
		}

		#region Abstract methods
		/// <summary>
		/// Gets the entity query for the given key.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected abstract Expression<Func<TEntity, bool>> GetByKey(string key);
		#endregion
	}
}