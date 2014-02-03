/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

using AutoMapper;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Default implementation of the post repository.
	/// </summary>
	internal class PostRepository : SlugRepository<Models.Post, Entities.Post>, IPostRepository
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		public PostRepository(Db db) 
			: base(db, App.Instance.ModelCache.Posts, Hooks.Blog.Model.Post.OnLoad) { }

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <param name="limit">Result limit</param>
		/// <returns>Theh posts</returns>
		public IList<Models.Post> Get(Expression<Func<Entities.Post, bool>> predicate, int limit) {
			return Get(predicate).Take(limit).ToList();
		}

		/// <summary>
		/// Gets the posts matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <param name="limit">Result limit</param>
		/// <returns>Theh posts</returns>
		public async Task<IList<Models.Post>> GetAsync(Expression<Func<Entities.Post, bool>> predicate, int limit) {
			return (await GetAsync(predicate)).Take(limit).ToList();
		}

		/// <summary>
		/// Publishes the post with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="date">The optional publish date</param>
		public void Publish(Guid id, DateTime? date = null) {
			var post = uow.Posts.Where(p => p.Id == id).Single();
			post.Published = (date.HasValue ? date.Value : DateTime.Now);
		}

		/// <summary>
		/// Publishes the post with the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="date">The optional publish date</param>
		public async Task PublishAsync(Guid id, DateTime? date = null) {
			var post = await uow.Posts.Where(p => p.Id == id).SingleAsync();
			post.Published = (date.HasValue ? date.Value : DateTime.Now);
		}

		/// <summary>
		/// Maps the given entity to a model.
		/// </summary>
		/// <param name="entity">The entity</param>
		/// <returns>The mapped model</returns>
		protected override Models.Post Map(Entities.Post entity) {
			var converter = new MarkdownSharp.Markdown();

			var model = Mapper.Map<Entities.Post, Models.Post>(entity);
			model.Categories = model.Categories.OrderBy(c => c.Name).ToList();
			model.Tags = model.Tags.OrderBy(t => t.Name).ToList();
			model.Html = new HtmlString(converter.Transform(model.Body));

			return model;
		}

		/// <summary>
		/// Performs any additional processing after the model has been fetched
		/// and added to cache.
		/// </summary>
		/// <param name="model">The model</param>
		protected override void Process(Models.Post model) {
			var comments = new CommentRepository(uow);
			model.Comments = comments.GetByPostId(model.Id.Value);
		}

		/// <summary>
		/// Gets the entity query for the given key.
		/// </summary>
		/// <param name="key">The unique key</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Post, bool>> GetByKey(string key) {
			return p => p.Slug == key;
		}

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Post, bool>> Get(Models.Post model) {
			return p => p.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Post, bool>> Get(Guid id) {
			return p => p.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Post> GetQuery(Expression<Func<Entities.Post, bool>> predicate = null) {
			var query = uow.Posts
				.Include(p => p.Author)
				.Include(p => p.Categories)
				.Include(p => p.Comments)
				.Include(p => p.Tags);
			if (predicate != null)
				query = query.Where(predicate);
			return query.OrderByDescending(p => p.Published);
		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Post entity) {
			uow.Posts.Remove(entity);
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Post source, Entities.Post dest) {
			var state = new Models.ModelState();

			// Format slug
			if (String.IsNullOrEmpty(source.Slug))
				source.Slug = Utils.GenerateSlug(source.Title);
			else source.Slug = Utils.GenerateSlug(source.Slug);

			// Execute hooks
			if (Hooks.Blog.Model.Post.OnSave != null)
				Hooks.Blog.Model.Post.OnSave(source, state);

			// Proceed if state is valid
			if (state.IsValid) {
				if (dest == null) {
					dest = new Entities.Post();
					source.Id = dest.Id = Guid.NewGuid();

					uow.Posts.Add(dest);
				}

				// Save author
				if (source.Author != null) {
					using (var api = new Api()) {
						api.Authors.Add(source.Author);
						api.SaveChanges();
					}
				}

				// Map author
				if (source.Author != null)
					dest.Author = uow.Authors.Where(a => a.Id == source.Author.Id).Single();

				// Save categories & tags
				using (var api = new Api()) {
					foreach (var c in source.Categories)
						api.Categories.Add(c);
					foreach (var t in source.Tags)
						api.Tags.Add(t);
					api.SaveChanges();
				}

				// Map model
				Mapper.Map<Models.Post, Entities.Post>(source, dest);

				// Map categories
				var ids = source.Categories.Select(c => c.Id.Value);
				var removedCat = dest.Categories.Where(c => !ids.Contains(c.Id)).ToList();
				foreach (var cat in removedCat)
					dest.Categories.Remove(cat);
				foreach (var cat in source.Categories)
					if (dest.Categories.Where(c => c.Id == cat.Id).Count() == 0)
						dest.Categories.Add(uow.Categories.Where(c => c.Id == cat.Id).Single());

				// Map tags
				ids = source.Tags.Select(t => t.Id.Value);
				var removedTags = dest.Tags.Where(t => !ids.Contains(t.Id)).ToList();
				foreach (var tag in removedTags)
					dest.Tags.Remove(tag);
				foreach (var tag in source.Tags)
					if (dest.Tags.Where(t => t.Id == tag.Id).Count() == 0)
						dest.Tags.Add(uow.Tags.Where(t => t.Id == tag.Id).Single());
			} else { 
				throw new Models.ModelStateException("Error while adding post. See data for details", state);
			}
		}
	}
}
