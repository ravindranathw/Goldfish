using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories
{
	public interface ITagRepository
	{
		/// <summary>
		/// Gets the tag model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Models.Tag GetById(Guid id);

		/// <summary>
		/// Gets the tag model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Task<Models.Tag> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the tag model with the given slug. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		Models.Tag GetBySlug(string slug);

		/// <summary>
		/// Gets the tag model with the given slug. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		Task<Models.Tag> GetBySlugAsync(string slug);

		/// <summary>
		/// Gets the tag models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		IList<Models.Tag> Get(Expression<Func<Entities.Tag, bool>> predicate = null);

		/// <summary>
		/// Gets the tag models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		Task<IList<Models.Tag>> GetAsync(Expression<Func<Entities.Tag, bool>> predicate = null);

		/// <summary>
		/// Adds the given tag.
		/// </summary>
		/// <param name="model">The tag</param>
		void Add(Models.Tag model);

		/// <summary>
		/// Removes the tag with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(Guid id);

		/// <summary>
		/// Removes the given tag.
		/// </summary>
		/// <param name="model">The tag</param>
		void Remove(Models.Tag model);
	}
}
