using System;

namespace Goldfish.Config
{
	/// <summary>
	/// Configuration parameters for the core application.
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The different parameters available for caching.
		/// </summary>
		public static class Cache
		{
			/// <summary>
			/// Gets/sets if the cache is enabled.
			/// </summary>
			public static bool IsEnabled { 
				get { return Utils.GetParam<bool>("CACHE_ENABLED", s => Convert.ToBoolean(s)); }
				set { Utils.SetParam("CACHE_ENABLED", value); }
			}

			/// <summary>
			/// Gets/sets the expiration time in minutes.
			/// </summary>
			public static int Expires {
				get { return Utils.GetParam<int>("CACHE_EXPIRES", s => Convert.ToInt32(s)); }
				set { Utils.SetParam("CACHE_EXPIRES", value); }
			}
		}

		/// <summary>
		/// Gets/sets the currently active theme.
		/// </summary>
		public static string Theme {
			get { return Utils.GetParam<string>("THEME", s => s); }
			set { Utils.SetParam("THEME", value); }
		}
	}
}