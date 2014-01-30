/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Goldfish.Models
{
	/// <summary>
	/// Posts are the main model used to represent content in Goldfish.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Post
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		[DataMember]
		public Guid? Id { get; internal set; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		[DataMember]
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		[DataMember]
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		[DataMember]
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the main body.
		/// </summary>
		[DataMember]
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the main body as html.
		/// </summary>
		[DataMember]
		public HtmlString Html { get; internal set; }

		/// <summary>
		/// Gets/sets the date the post was published.
		/// </summary>
		[DataMember]
		public DateTime? Published { get; internal set; }

		/// <summary>
		/// Gets/sets the date the post was last updated.
		/// </summary>
		[DataMember]
		public DateTime Updated { get; internal set; }

		/// <summary>
		/// Gets/sets the optional author.
		/// </summary>
		[DataMember]
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the current categories.
		/// </summary>
		[DataMember]
		public IList<Category> Categories { get; set; }

		/// <summary>
		/// Gets/sets the current tags.
		/// </summary>
		[DataMember]
		public IList<Tag> Tags { get; set; }

		/// <summary>
		/// Gets/sets the current comments.
		/// </summary>
		[DataMember]
		public IList<Comment> Comments { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Post() {
			Categories = new List<Category>();
			Comments = new List<Comment>();
			Tags = new List<Tag>();
		}
	}
}
