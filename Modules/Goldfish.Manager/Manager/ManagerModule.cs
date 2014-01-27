using System;
using System.ComponentModel.Composition;

namespace Goldfish.Manager
{
	/// <summary>
	/// The main manager module.
	/// </summary>
	[Export(typeof(Goldfish.Extend.IModule))]
	public class ManagerModule : Goldfish.Extend.IModule
	{
		/// <summary>
		/// Initializes the manager module.
		/// </summary>
		public void Init() {
			Goldfish.Hooks.App.Init.RegisterPrecompiledViews += assemblies => {
				assemblies.Add(typeof(Goldfish.Manager.ManagerModule).Assembly);
			};
		}
	}
}