using System;
using System.ComponentModel.Composition;
using Goldfish.Extend;

namespace Goldfish.Manager
{
	/// <summary>
	/// The main manager module.
	/// </summary>
	[Module(Name="Manager")]
	[Export(typeof(IModule))]
	public class ManagerModule : Module
	{
		/// <summary>
		/// Initializes the manager module.
		/// </summary>
		public override void Init() {
			Goldfish.Hooks.App.Init.RegisterPrecompiledViews += assemblies => {
				assemblies.Add(typeof(Goldfish.Manager.ManagerModule).Assembly);
			};
		}
	}
}