using System;

namespace Goldfish.Areas.Manager.Models.Account
{
	/// <summary>
	/// Post model for logging in.
	/// </summary>
	public class LoginModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the user name.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets/sets the password.
		/// </summary>
		public string Password { get; set; }
		#endregion
	}
}