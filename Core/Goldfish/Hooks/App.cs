using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Goldfish.Hooks
{
	/// <summary>
	/// The hooks available for the main application.
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The delegates specific for the main app.
		/// </summary>
		public static class Delegates
		{ 
			/// <summary>
			/// Delegate for seeding the database.
			/// </summary>
			/// <param name="db">The current db context</param>
			public delegate void DatabaseSeedDelegate(Goldfish.Db db);

			/// <summary>
			/// Delegate for configuring the database.
			/// </summary>
			/// <param name="config">The current database configuration</param>
			public delegate void DatabaseConfigDelegate(DbConfiguration config);

			/// <summary>
			/// Delegate for changing the default IoC registration.
			/// </summary>
			/// <param name="container">The current IoC container.</param>
			public delegate void IoCRegistrationDelegate(TinyIoCContainer container);
		}

		/// <summary>
		/// The hooks available for the database.
		/// </summary>
		public static class Db 
		{
			/// <summary>
			/// Called when the db configuration is created.
			/// </summary>
			public static Delegates.DatabaseConfigDelegate Configure;

			/// <summary>
			/// Called after the database has been created and seeded with default data.
			/// </summary>
			public static Delegates.DatabaseSeedDelegate Seed;
		}

		/// <summary>
		/// The hooks available for application init.
		/// </summary>
		public static class Init 
		{
			/// <summary>
			/// Called when the IoC container wants to register the api. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterApi;

			/// <summary>
			/// Called when the IoC container wants to register the cache. By
			/// implementing this hook the default registration is replaced.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate RegisterCache;

			/// <summary>
			/// Called when the IoC container has finished registering the default
			/// types. This hook can be used to register additional types in the
			/// IoC container.
			/// </summary>
			public static Delegates.IoCRegistrationDelegate Register;
		}

		/// <summary>
		/// The hooks available for the models.
		/// </summary>
		public static class Model
		{ 
			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
			public static Hooks.Delegates.ModelDelegate<Models.Param> OnParamLoad;
		}

		/// <summary>
		/// The hooks available for UI rendering.
		/// </summary>
		public static class UI 
		{ 
			/// <summary>
			/// Called when the core has finished rendering the basic meta tags. The output of
			/// this method is appended to the default meta data.
			/// </summary>
			public static Hooks.Delegates.OutputAppendDelegate GetMeta;
		}
	}
}