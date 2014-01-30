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
	public interface IAuthorRepository
	{
		/// <summary>
		/// Gets the author model with the given id. 
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Models.Author GetById(Guid id);

		/// <summary>
		/// Gets the author model with the given id. 
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Task<Models.Author> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the author models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		IList<Models.Author> Get(Expression<Func<Entities.Author, bool>> predicate = null);

		/// <summary>
		/// Gets the author models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		Task<IList<Models.Author>> GetAsync(Expression<Func<Entities.Author, bool>> predicate = null);

		/// <summary>
		/// Adds the given author.
		/// </summary>
		/// <param name="model">The Author</param>
		void Add(Models.Author model);

		/// <summary>
		/// Removes the author with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(Guid id);

		/// <summary>
		/// Removes the given author.
		/// </summary>
		/// <param name="model">The author</param>
		void Remove(Models.Author model);
	}
}
