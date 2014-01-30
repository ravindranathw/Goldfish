/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Threading.Tasks;

namespace Goldfish.Repositories
{
	public interface IArchiveRepository
	{
		/// <summary>
		/// Gets the post archive according to the given parameters.
		/// </summary>
		/// <param name="page">Optional page number</param>
		/// <param name="year">Optional year</param>
		/// <param name="month">Optional month</param>
		/// <returns>The archive</returns>
		Task<Models.Archive> GetArchiveAsync(int page = 1, int? year = null, int? month = null);

		/// <summary>
		/// Gets the archive for the category with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The archive</returns>
		Task<Models.CategoryArchive> GetCategoryArchiveAsync(string slug, int page = 1);

		/// <summary>
		/// Gets the archive for the tag with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The archive</returns>
		Task<Models.TagArchive> GetTagArchiveAsync(string slug, int page = 1);
	}
}
