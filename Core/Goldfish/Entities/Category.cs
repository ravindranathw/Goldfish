using System;

namespace Goldfish.Entities
{
	/// <summary>
	/// Categories are used to group posts together.
	/// </summary>
	public sealed class Category : BaseEntity<Category>, Cache.ICacheEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }
		#endregion

		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		public void RemoveFromCache() {
			App.Instance.EntityCache.Categories.Remove(Id);
		}
	}
}
