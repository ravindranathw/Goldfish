/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Default implementation of the comment repository.
	/// </summary>
	internal class CommentRepository : BaseRepository<Models.Comment, Entities.Comment>, ICommentRepository
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context.</param>
		public CommentRepository(Db db)
			: base(db, null, Hooks.Blog.Model.Comment.OnLoad) { }

		/// <summary>
		/// Gets the comments available for the post with the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The comments</returns>
		public IList<Models.Comment> GetByPostId(Guid id) {
			return Get(c => c.PostId == id);
		}

		/// <summary>
		/// Gets the comments available for the post with the given id.
		/// </summary>
		/// <param name="id">The post id</param>
		/// <returns>The comments</returns>
		public async Task<IList<Models.Comment>> GetByPostIdAsync(Guid id) {
			return await GetAsync(c => c.PostId == id);
		}

		/// <summary>
		/// Maps the given entity to a model.
		/// </summary>
		/// <param name="entity">The entity</param>
		/// <returns>The model</returns>
		protected override Models.Comment Map(Entities.Comment entity) {
			var model = Mapper.Map<Entities.Comment, Models.Comment>(entity);
			model.Html = new HtmlString(model.Body.Replace("\n", "<br>").GenerateLinks());

			return model;
		}

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Comment, bool>> Get(Models.Comment model) {
			return c => c.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Comment, bool>> Get(Guid id) {
			return c => c.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Comment> GetQuery(Expression<Func<Entities.Comment, bool>> predicate = null) {
			if (predicate != null)
				return uow.Comments
					.Where(predicate)
					.OrderBy(c => c.Created);
			else return uow.Comments
					.OrderBy(c => c.Created);
		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Comment entity) {
			uow.Comments.Remove(entity);
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Comment source, Entities.Comment dest) {
			var state = new Models.ModelState();

			// Make sure there's no html tags in the body
			source.Body = source.Body.StripHtml();

			// Execute hooks
			if (Hooks.Blog.Model.Comment.OnSave != null)
				Hooks.Blog.Model.Comment.OnSave(source, state);

			// Proceed if state is valid
			if (state.IsValid) {
				if (dest == null) {
					dest = new Entities.Comment() {
						PostId = source.PostId
					};
					uow.Comments.Add(dest);
				}
				Mapper.Map<Models.Comment, Entities.Comment>(source, dest);
			} else { 
				throw new Models.ModelStateException("Error while adding comment. See data for details", state);
			}
		}
	}
}