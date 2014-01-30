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
	/// Default implementation of the author repository.
	/// </summary>
	internal class AuthorRepository : BaseRepository<Models.Author, Entities.Author>, IAuthorRepository
	{ 
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current context</param>
		public AuthorRepository(Db db)
			: base(db, App.Instance.EntityCache.Authors, Hooks.Blog.Model.OnAuthorLoad) { }

		/// <summary>
		/// Gets the entity query for the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Author, bool>> Get(Models.Author model) {
			return a => a.Id == model.Id;
		}

		/// <summary>
		/// Gets the entity query for the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>The queryable</returns>
		protected override Expression<Func<Entities.Author, bool>> Get(Guid id) {
			return a => a.Id == id;
		}

		/// <summary>
		/// Gets the entity query for the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The queryable</returns>
		protected override IQueryable<Entities.Author> GetQuery(Expression<Func<Entities.Author, bool>> predicate = null) {
			if (predicate != null)
				return uow.Authors.Where(predicate).OrderBy(c => c.Name);
			else return uow.Authors.OrderBy(c => c.Name);
		}

		/// <summary>
		/// Removes the given entity.
		/// </summary>
		/// <param name="entity">The entity</param>
		protected override void Remove(Entities.Author entity) {
			uow.Authors.Remove(entity);
		}

		/// <summary>
		/// Adds the source model to the destination entity. If the destination
		/// is null a new entity is created and added.
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="dest">The destination</param>
		protected override void Add(Models.Author source, Entities.Author dest) {
			if (dest == null) { 
				dest = new Entities.Author();
				source.Id = dest.Id = Guid.NewGuid();

				uow.Authors.Add(dest);
			}
			Mapper.Map<Models.Author, Entities.Author>(source, dest);
		}
	}
}
