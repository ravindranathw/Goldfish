using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Goldfish.Cache;

namespace Goldfish.Blog.Controllers
{
	/// <summary>
	/// The main controller for Goldfish.
	/// </summary>
    public class BlogController : Controller
	{
		#region Members
		private readonly Goldfish.IApi Api;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="api">The api object</param>
		public BlogController(Goldfish.IApi api) {
			Api = api;
		}

		/// <summary>
		/// Default startpage route.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("~/")]
		public async Task<ActionResult> Start() {
			var model = await Api.Archives.GetArchiveAsync();
			Tools.Current = model;

			return View(model);
		}

		/// <summary>
		/// Gets the post with the given slug
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The view result</returns>
		[Route("~/{slug}")]
		public async Task<ActionResult> Post(string slug) {
			var model = await Api.Posts.GetBySlugAsync(slug);
			Tools.Current = model;

			if (model != null) {
				if (!HttpContext.IsCached(model.Id.Value.ToString(), model.Published.Value))
					return View(model);
				else return null;
			} else return View("NotFound");
		}

		/// <summary>
		/// Gets the post archive for the given period.
		/// </summary>
		/// <param name="year">Optional year</param>
		/// <param name="month">Optional month</param>
		/// <returns>The view result</returns>
		[Route("~/archive/{year:int?}/{month:int?}")]
		public async Task<ActionResult> Archive(int? year = null, int? month = null) {
			var model = await Api.Archives.GetArchiveAsync(1, year, month);
			Tools.Current = model;

			return View(model);
		}

		/// <summary>
		/// Gets the category archive for the category with
		/// the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The view result</returns>
		[Route("~/category/{slug}/{page:int?}")]
		public async Task<ActionResult> Category(string slug, int? page) {
			var model = await Api.Archives.GetCategoryArchiveAsync(slug, (page.HasValue ? page.Value : 1));
			Tools.Current = model;

			if (model != null)
				return View(model);
			return View("NotFound");
		}

		/// <summary>
		/// Gets the tag archive for the tag with the
		/// given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="page">Optional page number</param>
		/// <returns>The view result</returns>
		[Route("~/tag/{slug}/{page:int?}")]
		public async Task<ActionResult> Tag(string slug, int? page) {
			var model = await Api.Archives.GetTagArchiveAsync(slug, (page.HasValue ? page.Value : 1));
			Tools.Current = model;

			if (model != null)
				return View(model);
			return View("NotFound");
		}

		/// <summary>
		/// Gets the media element with the given path.
		/// </summary>
		/// <param name="path">The media path</param>
		/// <returns>The file result</returns>
		[Route("~/media/{path}")]
		public ActionResult Media(string path) {
			return null;
		}

		/// <summary>
		/// Gets the rss feed for the specified resource.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("~/feed/rss")]
		public async Task<ActionResult> Rss() {
			return new Goldfish.Web.Mvc.RssResult((await Api.Posts.GetAsync()));
		}

		/// <summary>
		/// Gets the atom feed for the specified resource.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("~/feed/atom")]
		public async Task<ActionResult> Atom() {
			return new Goldfish.Web.Mvc.AtomResult((await Api.Posts.GetAsync()));
		}

		/// <summary>
		/// Adds a new comment.
		/// </summary>
		/// <param name="model">The comment model</param>
		/// <returns>The view result</returns>
		[Route("~/comment")]
		[HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddComment(Goldfish.Models.Comment model) {
			if (ModelState.IsValid) {
				model.IP = HttpContext.Request.UserHostAddress;
				model.SessionID = Session.SessionID;
				model.IsApproved = true;
				if (User.Identity.IsAuthenticated)
					model.UserId = User.Identity.Name;

				if (User.Identity.IsAuthenticated && Config.Blog.Comments.ModerateAuthorized)
					model.IsApproved = false;
				else if (!User.Identity.IsAuthenticated && Config.Blog.Comments.ModerateAnonymous)
					model.IsApproved = false;

				Api.Comments.Add(model);
				Api.SaveChanges();

				var post = await Api.Posts.GetByIdAsync(model.PostId);

				return RedirectToAction("Post", new { slug = post.Slug });
			}
			return null;
		}

		/// <summary>
		/// Disposes the controller and all of its resources.
		/// </summary>
		/// <param name="disposing">If the controller should dispose managed objects</param>
		protected override void Dispose(bool disposing) {
			// Dispose the current api
			Api.Dispose();

			// Disposes the controller itself
			base.Dispose(disposing);
		}
	}
}