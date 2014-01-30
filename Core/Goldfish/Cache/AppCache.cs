using System;

namespace Goldfish.Cache
{
	/// <summary>
	/// The main entity cache object.
	/// </summary>
	internal class AppCache
	{
		#region Members
		/// <summary>
		/// The author cache.
		/// </summary>
		public readonly ModelCache<Models.Author> Authors;

		/// <summary>
		/// The category cache.
		/// </summary>
		public readonly ModelCache<Models.Category> Categories;

		/// <summary>
		/// The param cache.
		/// </summary>
		public readonly ModelCache<Models.Param> Params;

		/// <summary>
		/// The post cache.
		/// </summary>
		public readonly ModelCache<Models.Post> Posts;

		/// <summary>
		/// The tag cache.
		/// </summary>
		public readonly ModelCache<Models.Tag> Tags;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="container">The IoC container</param>
		public AppCache(ICacheProvider provider) {
			Authors = new ModelCache<Models.Author>(provider, a => a.Id.Value, a => a.Email);
			Categories = new ModelCache<Models.Category>(provider, c => c.Id.Value, c => c.Slug);
			Params = new ModelCache<Models.Param>(provider, p => p.Id.Value, p => p.InternalId);
			Posts = new ModelCache<Models.Post>(provider, p => p.Id.Value, p => p.Slug);
			Tags = new ModelCache<Models.Tag>(provider, t => t.Id.Value, t => t.Slug);
		}
	}
}
