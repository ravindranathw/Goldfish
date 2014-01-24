using System;
using System.Text;

namespace Goldfish.Hooks
{
	/// <summary>
	/// The standard delegates available.
	/// </summary>
	public static class Delegates
	{
		/// <summary>
		/// Delegate for manipulating a model.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="model">The model</param>
		public delegate void ModelDelegate<T>(T model);

		/// <summary>
		/// Delegate for returning a string to be rendered.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="model">The model</param>
		/// <returns>The rendered output</returns>
		public delegate string OutputReturnDelegate<T>(T model);

		/// <summary>
		/// Delegate for appending rendered output to a string builder.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="sb">The string builder</param>
		/// <param name="model">The model</param>
		public delegate void OutputAppendDelegate<T>(StringBuilder sb, T model);

		/// <summary>
		/// Delegate for appending rendered output to a string builder.
		/// </summary>
		/// <param name="sb">The string builder</param>			/// <param name="model">The model</param>
		public delegate void OutputAppendDelegate(StringBuilder sb);
	}
}