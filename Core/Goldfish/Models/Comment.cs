/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Runtime.Serialization;
using System.Web;

namespace Goldfish.Models
{
	/// <summary>
	/// Comments are used for discussing posts.
	/// </summary>
	[Serializable]
	[DataContract]
	public sealed class Comment
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		[DataMember]
		public Guid? Id { get; set; }

		/// <summary>
		/// Gets/sets the post id.
		/// </summary>
		[DataMember]
		public Guid PostId { get; set; }

		/// <summary>
		/// Gets/sets the optional user id.
		/// </summary>
		[DataMember]
		public string UserId { get; set; }

		/// <summary>
		/// Gets/sets the author name.
		/// </summary>
		[DataMember]
		public string Author { get; set; }

		/// <summary>
		/// Gets/sets the author email.
		/// </summary>
		[DataMember]
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the optional author website.
		/// </summary>
		[DataMember]
		public string WebSite { get; set; }

		/// <summary>
		/// Gets/sets the comment body.
		/// </summary>
		[DataMember]
		public string Body { get; set; }
		[DataMember]

		/// <summary>
		/// Gets/sets the comment body as html.
		/// </summary>
		public HtmlString Html { get; internal set; }

		/// <summary>
		/// Gets/sets the optional IP adress from where the comment was made.
		/// </summary>
		[DataMember]
		public string IP { get; set; }

		/// <summary>
		/// Gets/sets the optional Session ID that made the comment.
		/// </summary>
		[DataMember]
		public string SessionID { get; set; }

		/// <summary>
		/// Gets/sets if the comment is approved or not.
		/// </summary>
		[DataMember]
		public bool IsApproved { get; set; }

		/// <summary>
		/// Gets/sets when the comment was created.
		/// </summary>
		[DataMember]
		public DateTime? Created { get; set; }
		#endregion
	}
}