using System;

namespace Goldfish.Entities
{
	/// <summary>
	/// Tags are used to categorize the content of a post.
	/// </summary>
	public sealed class Tag : BaseEntity<Tag>, Cache.ICacheEntity
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
			App.Instance.EntityCache.Tags.Remove(Id);
		}
	}
}
