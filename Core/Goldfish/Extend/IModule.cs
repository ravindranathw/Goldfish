using System;

namespace Goldfish.Extend
{
	/// <summary>
	/// Interface for a module.
	/// </summary>
	public interface IModule
	{
		string Name { get; }
		bool HasConfig { get; }
		Type ConfigType { get; }

		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		void Init();
	}
}
