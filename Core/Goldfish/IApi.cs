/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish
{
	/// <summary>
	/// Interface defining the methods that should be provided by
	/// the main api.
	/// </summary>
	public interface IApi : IDisposable
	{
		#region Properties
		/// <summary>
		/// Gets the archive repository.
		/// </summary>
		Repositories.IArchiveRepository Archives { get; }

		/// <summary>
		/// Gets the author repository.
		/// </summary>
		Repositories.IAuthorRepository Authors { get; }

		/// <summary>
		/// Gets the category repository.
		/// </summary>
		Repositories.ICategoryRepository Categories { get; }

		/// <summary>
		/// Gets the comment repository.
		/// </summary>
		Repositories.ICommentRepository Comments { get; }

		/// <summary>
		/// Gets the param repository.
		/// </summary>
		Repositories.IParamRepository Params { get; }

		/// <summary>
		/// Gets the post repository.
		/// </summary>
		Repositories.IPostRepository Posts { get; }

		/// <summary>
		/// Gets the tag repository.
		/// </summary>
		Repositories.ITagRepository Tags { get; }
		#endregion

		/// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of rows affected</returns>
		int SaveChanges();
	}
}
