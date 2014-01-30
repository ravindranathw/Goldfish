/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish.Entities
{
	/// <summary>
	/// Comments are used for discussing posts.
	/// </summary>
	public sealed class Comment : BaseEntity<Comment>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the post id.
		/// </summary>
		public Guid PostId { get; set; }

		/// <summary>
		/// Gets/sets the optional user id.
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Gets/sets the author name.
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Gets/sets the author email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the optional author website.
		/// </summary>
		public string WebSite { get; set; }

		/// <summary>
		/// Gets/sets the comment body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the optional IP adress from where the comment was made.
		/// </summary>
		public string IP { get; set; }

		/// <summary>
		/// Gets/sets the optional Session ID that made the comment.
		/// </summary>
		public string SessionID { get; set; }

		/// <summary>
		/// Gets/sets if the comment is approved or not.
		/// </summary>
		public bool IsApproved { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the post.
		/// </summary>
		public Post Post { get; set; }
		#endregion
	}
}