using System;
using System.Collections.Generic;

namespace Goldfish.Cache
{
	/// <summary>
	/// Interface for creating an application cache provider.
	/// </summary>
	public interface ICacheProvider
	{
		/// <summary>
		/// Gets or creates the keymap for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The keymap</returns>
		Dictionary<string, Guid> GetKeyMap(string id);

		/// <summary>
		/// Sets the keymap for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="keyMap">The key map</param>
		void SetKeyMap(string id, Dictionary<string, Guid> keyMap);

		/// <summary>
		/// Gets the cached model for the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		T Get<T>(string id);

		/// <summary>
		/// Sets the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="obj">The model</param>
		void Set(string id, object obj);

		/// <summary>
		/// Removes the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(string id);
	}
}
