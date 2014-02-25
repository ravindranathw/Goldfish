/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Goldfish.Cache;

namespace Goldfish.Pages.Controllers
{
	/// <summary>
	/// The main controller for the pages module.
	/// </summary>
	[RoutePrefix("")]
    public class PagesController : Controller
    {
		/// <summary>
		/// Gets the page with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The view result</returns>
		[Route("{slug}")]
        public async Task<ActionResult> Default(string slug) {
			using (var api = new Api()) {
				var model = await api.Pages.GetBySlugAsync(slug);

				if (model != null && model.Published.HasValue && model.Published.Value <= DateTime.Now) {
					if (!HttpContext.IsCached(model.Id.ToString(), model.Published.Value)) {
						if (!String.IsNullOrEmpty(model.View))
							return View(model.View, model);
						return View(model);
					} else return null;
				} else return View("NotFound");
			}
        }
	}
}