/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AutoMapper;
using Goldfish.Extend;

namespace Goldfish.Pages
{
	/// <summary>
	/// Starts the pages module.
	/// </summary>
	[Export(typeof(IModule))]
	public class PagesModule : Module
	{
		#region Members
		internal const string CACHE_NAVIGATION_KEY = "PAGES_NAVIGATION_CACHE";
		#endregion

		/// <summary>
		/// Initializes the pages module.
		/// </summary>
		public override void Init() {
			// Register pre-compiled views
			Hooks.App.Init.RegisterPrecompiledViews += assemblies => {
				assemblies.Add(typeof(PagesModule).Assembly);
			};

			// Add cache
			AddCache<Page>(p => p.Id, p => p.Slug);

			// Intialize mappings
			Mapper.CreateMap<Page, Page>()
				.ForMember(e => e.Id, o => o.Ignore());

			Mapper.AssertConfigurationIsValid();
		}
	}
}