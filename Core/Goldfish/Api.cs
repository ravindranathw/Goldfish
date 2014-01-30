/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfish
{
	/// <summary>
	/// Default implementation of the application api.
	/// </summary>
	public sealed class Api : IApi
	{
		#region Members
		private readonly Db db;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the archive repository.
		/// </summary>
		public Repositories.IArchiveRepository Archives { get; private set; }

		/// <summary>
		/// Gets the author repository.
		/// </summary>
		public Repositories.IAuthorRepository Authors { get; private set; }

		/// <summary>
		/// Gets the category repository.
		/// </summary>
		public Repositories.ICategoryRepository Categories { get; private set; }

		/// <summary>
		/// Gets the comment repository.
		/// </summary>
		public Repositories.ICommentRepository Comments { get; private set; }

		/// <summary>
		/// Gets the param repository.
		/// </summary>
		public Repositories.IParamRepository Params { get; private set; }

		/// <summary>
		/// Gets the post repository.
		/// </summary>
		public Repositories.IPostRepository Posts { get; private set; }

		/// <summary>
		/// Gets the tag repository.
		/// </summary>
		public Repositories.ITagRepository Tags { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Api() : this(null) {}

		/// <summary>
		/// Creates a new api on an existing context.
		/// </summary>
		/// <param name="uow">The current db context</param>
		public Api(Db uow) {
			db = uow != null ? uow : new Db();

			Archives = new Repositories.Default.ArchiveRepository(db);
			Authors = new Repositories.Default.AuthorRepository(db);
			Categories = new Repositories.Default.CategoryRepository(db);
			Comments = new Repositories.Default.CommentRepository(db);
			Params = new Repositories.Default.ParamRepository(db);
			Posts = new Repositories.Default.PostRepository(db);
			Tags = new Repositories.Default.TagRepository(db);
		}

		/// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of rows affected</returns>
		public int SaveChanges() {
			return db.SaveChanges();
		}

		/// <summary>
		/// Disposes the api.
		/// </summary>
		public void Dispose() {
			db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
