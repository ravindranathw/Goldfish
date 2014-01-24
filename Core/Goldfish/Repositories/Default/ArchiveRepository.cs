using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Goldfish.Repositories.Default
{
	/// <summary>
	/// Default implementation of the archive repository.
	/// </summary>
	internal class ArchiveRepository : IArchiveRepository
	{
		#region Members
		private readonly Db uow;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="db">The current db context</param>
		public ArchiveRepository(Db db) {
			uow = db;
		}

		/// <summary>
		/// Gets the post archive according to the given parameters.
		/// </summary>
		/// <param name="page">Optional page number</param>
		/// <param name="year">Optional year</param>
		/// <param name="month">Optional month</param>
		/// <returns>The archive</returns>
		public async Task<Models.Archive> GetArchiveAsync(int page = 1, int? year = null, int? month = null) {
			var model = new Models.Archive() {
				Year = year,
				Month = month
			};

			var rep = new PostRepository(uow);

			DateTime? start = null;
			DateTime? stop = null;

			// Get start and stop dates
			if (year.HasValue) {
				start = new DateTime(year.Value, 1, 1);
				stop = start.Value.AddYears(1);

				if (month.HasValue) {
					start = new DateTime(year.Value, month.Value, 1);
					stop = start.Value.AddMonths(1);
				}
				model.TotalCount = await uow.Posts.Where(p => p.Published.HasValue && p.Published >= start && p.Published < stop).CountAsync();
			} else {
				model.TotalCount = await uow.Posts.Where(p => p.Published.HasValue).CountAsync();
			}

			// Calculate paging information
			model.PageSize = Config.Blog.ArchivePageSize;
			model.PageCount = Math.Max(Convert.ToInt32(Math.Ceiling(((double)model.TotalCount) / model.PageSize)), 1);
			model.CurrentPage = Math.Min(page, model.PageCount);

			// Get the posts
			if (start.HasValue)	{
				model.Posts = await rep.GetAsync(p => p.Published.HasValue && p.Published >= start && p.Published < stop, 
					model.CurrentPage * model.PageSize);
			} else { 
				model.Posts = await rep.GetAsync(p => p.Published.HasValue, model.CurrentPage * model.PageSize);
			}

			// Filter out the current page
			model.Posts = model.Posts.Subset((model.CurrentPage - 1) * model.PageSize).ToList();

			return model;
		}

		/// <summary>
		/// Gets the archive for the category with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The archive</returns>
		public async Task<Models.CategoryArchive> GetCategoryArchiveAsync(string slug, int page = 1) {
			var model = new Models.CategoryArchive();

			var categoryRep = new CategoryRepository(uow);
			var postRep = new PostRepository(uow);

			model.Category = await categoryRep.GetBySlugAsync(slug);

			if (model.Category != null) {
				// Fill model
				model.TotalCount = await uow.Posts.Where(p => p.Published.HasValue && p.Categories.Any(c => c.Slug == slug)).CountAsync();
				model.PageSize = Config.Blog.ArchivePageSize;
				model.PageCount = Math.Max(Convert.ToInt32(Math.Ceiling(((double)model.TotalCount) / model.PageSize)), 1);
				model.CurrentPage = Math.Min(page, model.PageCount);
				model.Posts = await postRep.GetAsync(p => p.Published.HasValue && p.Categories.Any(c => c.Slug == slug), model.CurrentPage * model.PageSize);

				// Filter out the current page
				model.Posts = model.Posts.Subset((model.CurrentPage - 1) * model.PageSize).ToList();

				return model;
			}
			return null;
		}

		/// <summary>
		/// Gets the archive for the tag with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The archive</returns>
		public async Task<Models.TagArchive> GetTagArchiveAsync(string slug, int page = 1) { 
			var model = new Models.TagArchive();

			var tagRep = new TagRepository(uow);
			var postRep = new PostRepository(uow);

			model.Tag = await tagRep.GetBySlugAsync(slug);

			if (model.Tag != null) {
				// Fill model
				model.TotalCount = await uow.Posts.Where(p => p.Published.HasValue && p.Tags.Any(t => t.Slug == slug)).CountAsync();
				model.PageSize = Config.Blog.ArchivePageSize;
				model.PageCount = Math.Max(Convert.ToInt32(Math.Ceiling(((double)model.TotalCount) / model.PageSize)), 1);
				model.CurrentPage = Math.Min(page, model.PageCount);
				model.Posts = await postRep.GetAsync(p => p.Published.HasValue && p.Tags.Any(t => t.Slug == slug), model.CurrentPage * model.PageSize);

				// Filter out the current page
				model.Posts = model.Posts.Subset((model.CurrentPage - 1) * model.PageSize).ToList();

				return model;
			}
			return null;
		}
	}
}
