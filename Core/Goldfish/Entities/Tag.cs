﻿/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;

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
		[Index("IX_TagSlug", IsUnique=true)]
		public string Slug { get; set; }
		#endregion

		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		public void RemoveFromCache() {
			App.Instance.ModelCache.Tags.Remove(Id);
		}
	}
}
