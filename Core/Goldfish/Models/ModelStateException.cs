/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish.Models
{
	/// <summary>
	/// Exception for errors returned by the save hooks.
	/// </summary>
	public class ModelStateException : Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="state">The model state that caused the exception</param>
		public ModelStateException(string message, ModelState state) : base(message) {
			// Add all of the reported errors.
			foreach (var error in state.Errors) {
				this.Data.Add(error.Sender.GetType().FullName,
					error.Message);
			}
		}
	}
}