using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Goldfish.Entities
{
	/// <summary>
	/// Main application user.
	/// </summary>
	public sealed class User : IdentityUser
	{
		/// <summary>
		/// Gets/sets the email.
		/// </summary>
		public string Email { get; set; }
	}
}