/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;

namespace Goldfish.Entities
{
	/// <summary>
	/// The main post entity.
	/// </summary>
	public sealed class Post : BaseEntity<Post>, Cache.ICacheEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the optional author.
		/// </summary>
		public Guid? AuthorId { get; set; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the main body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the date the post was published.
		/// </summary>
		public DateTime? Published { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the optional author.
		/// </summary>
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the available categories.
		/// </summary>
		public IList<Category> Categories { get; set; }

		/// <summary>
		/// Gets/sets the available tags.
		/// </summary>
		public IList<Tag> Tags { get; set; }

		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		public IList<Comment> Comments { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Post() {
			Categories = new List<Category>();
			Tags = new List<Tag>();
			Comments = new List<Comment>();
		}

		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		public void RemoveFromCache() {
			App.Instance.ModelCache.Posts.Remove(Id);
		}
	}
}
