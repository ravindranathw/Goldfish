/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories
{
	public interface ICategoryRepository
	{
		/// <summary>
		/// Gets the category model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Models.Category GetById(Guid id);

		/// <summary>
		/// Gets the category model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Task<Models.Category> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the category model with the given slug. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The models</returns>
		Models.Category GetBySlug(string slug);

		/// <summary>
		/// Gets the category model with the given slug. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The models</returns>
		Task<Models.Category> GetBySlugAsync(string slug);

		/// <summary>
		/// Gets the category models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		IList<Models.Category> Get(Expression<Func<Entities.Category, bool>> predicate = null);

		/// <summary>
		/// Gets the category models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		Task<IList<Models.Category>> GetAsync(Expression<Func<Entities.Category, bool>> predicate = null);

		/// <summary>
		/// Adds the given category.
		/// </summary>
		/// <param name="model">The category</param>
		void Add(Models.Category model);

		/// <summary>
		/// Removes the category with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(Guid id);

		/// <summary>
		/// Removes the given category.
		/// </summary>
		/// <param name="model">The category</param>
		void Remove(Models.Category model);
	}
}
