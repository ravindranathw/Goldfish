/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Goldfish
{
	/// <summary>
	/// Security, user and role management.
	/// </summary>
	public sealed class SecurityManager
	{
		#region Properties
		/// <summary>
		/// The user manager.
		/// </summary>
		private UserManager<Entities.User> UserManager { get; set; }

		/// <summary>
		/// The role store.
		/// </summary>
		private RoleStore<IdentityRole> RoleStore { get; set; }
		private readonly Db db; 

		/// <summary>
		/// The authentication manager.
		/// </summary>
		private IAuthenticationManager Authentication {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SecurityManager(Db db = null) {
			if (db == null)
				db = new Db();
			this.db = db;

			UserManager = new UserManager<Entities.User>(new UserStore<Entities.User>(this.db));
			RoleStore = new RoleStore<IdentityRole>(this.db);
		}

		/// <summary>
		/// Authenticates and returns the user with the given credentials.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the user was successfully authenticated</returns>
		public bool Authenticate(string username, string password) {
			var user = new Entities.User();

			return UserManager.Find(username, password) != null;
		}

		/// <summary>
		/// Authenticates and signs in the user with the given credentials.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the user was signed in</returns>
		public bool SignIn(string username, string password) {
			var user = UserManager.Find(username, password);

			if (user != null) {
				Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
				var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
				Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Signs in the current user.
		/// </summary>
		public void SignOut() {
            Authentication.SignOut();
		}

		/// <summary>
		/// Creates a new user with the given roles.
		/// </summary>
		/// <param name="user">The user</param>
		/// <param name="password">The desired password</param>
		/// <param name="roles">The roles</param>
		/// <returns>If the user was created</returns>
        public bool Create(Entities.User user, string password, params string[] roles) {
            var result = UserManager.Create(user, password);

            if (result.Succeeded) {
				if (roles != null && roles.Length > 0)
	                AddUserToRoles(user, roles);
				return true;
            } else {
				return false;
			} 
        }

		/// <summary>
		/// Checks if the given user has the specified role.
		/// </summary>
		/// <param name="user">The user</param>
		/// <param name="role">The roles</param>
		/// <returns>If the user has the given role</returns>
		public bool IsInRole(Entities.User user, string role) {
			return UserManager.IsInRole(user.Id, role);
		}

		/// <summary>
		/// Gets the currently available roles.
		/// </summary>
		/// <returns></returns>
		public string[] GetRoles() {
			return RoleStore.Roles.Select(r => r.Name).ToArray();
		}

		/// <summary>
		/// Creates a new role.
		/// </summary>
		/// <param name="name"></param>
		public void CreateRole(string name) {
			db.Roles.Add(new IdentityRole() {
				Id = Guid.NewGuid().ToString(),
				Name = name
			});
			db.SaveChanges();
		}

		/// <summary>
		/// Adds the given user to the given roles.
		/// </summary>
		/// <param name="user">The user</param>
		/// <param name="roles">The roles</param>
		public void AddUserToRoles(Entities.User user, params string[] roles) {
            foreach (var role in roles) {
                UserManager.AddToRole(user.Id, role);
            }
		}

		/// <summary>
		/// Removes the user from the given roles.
		/// </summary>
		/// <param name="user">The user</param>
		/// <param name="roles">The roles</param>
		public void RemoveUserFromRoles(Entities.User user, params string[] roles) {
			foreach (var role in roles) {
				UserManager.RemoveFromRole(user.Id, role);
			}
		}
	}
}