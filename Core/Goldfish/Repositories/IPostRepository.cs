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
	/// <summary>
	/// The post repository defines the different methods available for posts.
	/// </summary>
	public interface IPostRepository
	{
		/// <summary>
		/// Gets the post with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The post</returns>
		Models.Post GetById(Guid id);

		/// <summary>
		/// Gets the post with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The post</returns>
		Task<Models.Post> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the post with the given unique slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The post</returns>
		Models.Post GetBySlug(string slug);

		/// <summary>
		/// Gets the post with the given unique slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The post</returns>
		Task<Models.Post> GetBySlugAsync(string slug);

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>Theh posts</returns>
		IList<Models.Post> Get(Expression<Func<Entities.Post, bool>> predicate = null);

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>Theh posts</returns>
		Task<IList<Models.Post>> GetAsync(Expression<Func<Entities.Post, bool>> predicate = null);

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <param name="limit">Result limit</param>
		/// <returns>Theh posts</returns>
		IList<Models.Post> Get(Expression<Func<Entities.Post, bool>> predicate, int limit);

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <param name="limit">Result limit</param>
		/// <returns>Theh posts</returns>
		Task<IList<Models.Post>> GetAsync(Expression<Func<Entities.Post, bool>> predicate, int limit);

		/// <summary>
		/// Adds the given post.
		/// </summary>
		/// <param name="model">The post</param>
		void Add(Models.Post model);

		/// <summary>
		/// Removes the post with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(Guid id);

		/// <summary>
		/// Removes the given post.
		/// </summary>
		/// <param name="model">The post</param>
		void Remove(Models.Post model);

		/// <summary>
		/// Publishes the post with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="date">The optional publish date</param>
		void Publish(Guid id, DateTime? date = null);
	}
}
