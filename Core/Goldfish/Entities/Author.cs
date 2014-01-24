using System;
using System.Collections.Generic;

namespace Goldfish.Entities
{
	/// <summary>
	/// The autor is used to describe users writing posts.
	/// </summary>
	public sealed class Author : BaseEntity<Author>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the email adress.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the biography.
		/// </summary>
		public string Bio { get; set; }
		#endregion
	}
}
