/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Hooks
{
	/// <summary>
	/// The hooks available for the pages module.
	/// </summary>
	public static class Pages
	{
		/// <summary>
		/// The model hooks available.
		/// </summary>
		public static class Model 
		{
			/// <summary>
			/// Called after the model has been loaded from the database but before it
			/// has been put into the application cache.
			/// </summary>
			public static Delegates.ModelDelegate<Goldfish.Pages.Page> OnLoad;

			/// <summary>
			/// Called before the model is saved to the database.
			/// </summary>
			public static Delegates.ModelSaveDelegate<Goldfish.Pages.Page> OnSave;
		}
	}
}