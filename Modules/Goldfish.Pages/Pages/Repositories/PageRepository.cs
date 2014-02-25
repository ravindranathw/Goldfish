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
using System.Web;
using System.Threading.Tasks;
using AutoMapper;

namespace Goldfish.Pages.Repositories
{
	public class PageRepository
	{
		#region Members
		/// <summary>
		/// The current data context.
		/// </summary>
		private readonly Db uow;
		#endregion

		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="db">The data context</param>
		internal PageRepository(Db db) {
			uow = db;
		}

		/// <summary>
		/// Gets the page with the given slug.
		/// </summary>
		/// <param name="slug">The slug.</param>
		/// <returns>The page</returns>
		public Page GetBySlug(string slug) {
			var page = PagesModule.GetCache<Page>().Get(slug);

			if (page == null)
				return Get(p => p.Slug == slug).SingleOrDefault();
			return page;
		}

		/// <summary>
		/// Gets the page with the given slug.
		/// </summary>
		/// <param name="slug">The slug.</param>
		/// <returns>The page</returns>
		public async Task<Page> GetBySlugAsync(string slug) {
			var page = PagesModule.GetCache<Page>().Get(slug);

			if (page == null)
				return (await GetAsync(p => p.Slug == slug)).SingleOrDefault();
			return page;
		}

		/// <summary>
		/// Gets the pages matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The pages</returns>
		public IList<Page> Get(Expression<Func<Page, bool>> predicate = null) {
			IList<Page> pages = null;

			if (predicate != null)
				pages = uow.Pages.Where(predicate).OrderBy(p => p.Title).ToList();
			else pages = uow.Pages.OrderBy(p => p.Title).ToList();

			foreach (var p in pages) {
				p.Html = new HtmlString(Utils.TransformMarkdown(p.Body));

				// Execute hook
				if (Hooks.Pages.Model.OnLoad != null)
					Hooks.Pages.Model.OnLoad(p);

				// Add to cache
				PagesModule.GetCache<Page>().Add(p);
			}
			return pages;
		}

		/// <summary>
		/// Gets the pages matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The pages</returns>
		public async Task<IList<Page>> GetAsync(Expression<Func<Page, bool>> predicate = null) {
			IList<Page> pages = null;

			if (predicate != null)
				pages = await uow.Pages.Where(predicate).OrderBy(p => p.Title).ToListAsync();
			else pages = await uow.Pages.OrderBy(p => p.Title).ToListAsync();

			foreach (var p in pages) { 
				p.Html = new HtmlString(Utils.TransformMarkdown(p.Body));

				// Execute hook
				if (Hooks.Pages.Model.OnLoad != null)
					Hooks.Pages.Model.OnLoad(p);

				// Add to cache
				PagesModule.GetCache<Page>().Add(p);
			}
			return pages;
		}

		/// <summary>
		/// Adds a new or existing page.
		/// </summary>
		/// <param name="page">The page</param>
		public void Add(Page page) {
			var state = new Models.ModelState();

			// Ensure slug
			if (String.IsNullOrEmpty(page.Slug))
				page.Slug = Utils.GenerateSlug(page.Title);
			else page.Slug = Utils.GenerateSlug(page.Slug);

			// Execute hooks
			if (Hooks.Pages.Model.OnSave!= null)
				Hooks.Pages.Model.OnSave(page, state);

			if (state.IsValid) {
				var entity = uow.Pages.Where(p => p.Id == page.Id).SingleOrDefault();

				if (entity == null) {
					entity = new Page() {
						Id = Guid.NewGuid()
					};
					uow.Pages.Add(entity);
				}
				Mapper.Map<Page, Page>(page, entity);
			} else { 
				throw new Models.ModelStateException("Error while adding page. See data for details", state);
			}
		}

		/// <summary>
		/// Removes the block with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public void Remove(Guid id) {
			var entity = uow.Pages.Where(p => p.Id == id).Single();
			uow.Pages.Remove(entity);
		}
	}
}