using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories
{
	/// <summary>
	/// The comment repository defines the different methods available for comments.
	/// </summary>
	public interface ICommentRepository
	{
		/// <summary>
		/// Gets the comment with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The comment</returns>
		Models.Comment GetById(Guid id);

		/// <summary>
		/// Gets the comment with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The comment</returns>
		Task<Models.Comment> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the comment for the post with the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The comments</returns>
		IList<Models.Comment> GetByPostId(Guid id);

		/// <summary>
		/// Gets the comment for the post with the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The comments</returns>
		Task<IList<Models.Comment>> GetByPostIdAsync(Guid id);

		/// <summary>
		/// Gets the comments matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>Theh comments</returns>
		IList<Models.Comment> Get(Expression<Func<Entities.Comment, bool>> predicate = null);

		/// <summary>
		/// Gets the comments matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>Theh comments</returns>
		Task<IList<Models.Comment>> GetAsync(Expression<Func<Entities.Comment, bool>> predicate = null);

		/// <summary>
		/// Adds the given comment.
		/// </summary>
		/// <param name="model">The comment</param>
		void Add(Models.Comment model);

		/// <summary>
		/// Removes the comment with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(Guid id);

		/// <summary>
		/// Removes the given comment.
		/// </summary>
		/// <param name="model">The comment</param>
		void Remove(Models.Comment model);
	}
}
