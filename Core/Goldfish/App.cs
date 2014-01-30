/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web;
using AutoMapper;

namespace Goldfish
{
	/// <summary>
	/// The main application class containing all stuff not related to the
	/// current request or session.
	/// </summary>
	public sealed class App
	{
		#region Members
		/// <summary>
		/// The singleton instance of the application object.
		/// </summary>
		public static readonly App Instance = new App();

		/// <summary>
		/// Wheather or not the application has been initialized.
		/// </summary>
		private bool IsInitialized = false;

		/// <summary>
		/// Initialization mutex.
		/// </summary>
		private object mutex = new object();

		/// <summary>
		/// The main composition container.
		/// </summary>
		private CompositionContainer container;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current IoC container.
		/// </summary>
		public TinyIoCContainer IoCContainer { get; private set; }

		/// <summary>
		/// Gets the entity cache.
		/// </summary>
		internal Cache.AppCache ModelCache { get; private set; }

		/// <summary>
		/// The currently imported modules.
		/// </summary>
		[ImportMany(typeof(Extend.IModule))]
		public IEnumerable<Lazy<Extend.IModule>> Modules { get; set; }
		#endregion

		/// <summary>
		/// Default private constructor.
		/// </summary>
		private App() { 
			Initialize();
		}

		/// <summary>
		/// Initializes the application object.
		/// </summary>
		public static void Init() {
			Instance.Initialize();
		}

		#region Private methods
		/// <summary>
		/// Initializes the application object.
		/// </summary>
		private void Initialize() {
			if (!IsInitialized) {
				lock (mutex) {
					if (!IsInitialized) {
						// Create the IoC container
						IoCContainer = new TinyIoCContainer();

						// Register API
						if (Hooks.App.Init.RegisterApi != null)
							Hooks.App.Init.RegisterApi(IoCContainer);
						else IoCContainer.Register<IApi, Api>().AsMultiInstance();

						// Register cache
						if (Hooks.App.Init.RegisterCache != null)
							Hooks.App.Init.RegisterCache(IoCContainer);
						else IoCContainer.Register<Cache.ICacheProvider, Cache.HttpCache>();

						// Register additional types
						if (Hooks.App.Init.Register != null)
							Hooks.App.Init.Register(IoCContainer);

						// Create model cache
						ModelCache = IoCContainer.Resolve<Cache.AppCache>();

						// Compose parts
						var catalog = new AggregateCatalog();

						if (HttpContext.Current != null) {
							catalog.Catalogs.Add(new DirectoryCatalog("Bin"));
						} else {
							catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
						}
						container = new CompositionContainer(catalog);
						container.ComposeParts(this);

						// Entities => Models
						Mapper.CreateMap<Entities.Author, Models.Author>();
						Mapper.CreateMap<Entities.Category, Models.Category>();
						Mapper.CreateMap<Entities.Comment, Models.Comment>()
							.ForMember(m => m.Html, o => o.Ignore());
						Mapper.CreateMap<Entities.Param, Models.Param>();
						Mapper.CreateMap<Entities.Post, Models.Post>()
							.ForMember(m => m.Html, o => o.Ignore())
							.ForMember(m => m.Comments, o => o.Ignore());
						Mapper.CreateMap<Entities.Tag, Models.Tag>();

						// Models => Entities
						Mapper.CreateMap<Models.Author, Entities.Author>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore());
						Mapper.CreateMap<Models.Category, Entities.Category>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore());
						Mapper.CreateMap<Models.Comment, Entities.Comment>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.PostId, o => o.Ignore())
							.ForMember(e => e.Post, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore());
						Mapper.CreateMap<Models.Param, Entities.Param>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.InternalId, o => o.MapFrom(m => m.InternalId.ToUpper()))
							.ForMember(e => e.IsSystemParam, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore());
						Mapper.CreateMap<Models.Post, Entities.Post>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.AuthorId, o => o.Ignore())
							.ForMember(e => e.Author, o => o.Ignore())
							.ForMember(e => e.Categories, o => o.Ignore())
							.ForMember(e => e.Tags, o => o.Ignore())
							.ForMember(e => e.Comments, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore())
							.ForMember(e => e.Published, o => o.Ignore());
						Mapper.CreateMap<Models.Tag, Entities.Tag>()
							.ForMember(e => e.Id, o => o.Ignore())
							.ForMember(e => e.Created, o => o.Ignore())
							.ForMember(e => e.Updated, o => o.Ignore());

						// Assert configuration
						Mapper.AssertConfigurationIsValid();

						// Init modules
						foreach (var module in this.Modules)
							module.Value.Init();

						// Set state
						IsInitialized = true;
					}
				}
			}
		}
		#endregion
	}
}
