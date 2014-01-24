using System;

namespace Goldfish.Cache
{
	/// <summary>
	/// Interface for entities that are cached server-side by the application object.
	/// </summary>
	internal interface ICacheEntity
	{
		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		void RemoveFromCache();
	}
}
