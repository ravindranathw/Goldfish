/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;

namespace Goldfish.Models
{
	/// <summary>
	/// The modelstate is used to communicate errors from save hooks
	/// to the main repository layer.
	/// </summary>
	public class ModelState
	{
		#region Inner classes
		/// <summary>
		/// Model error with information about a specific error.
		/// </summary>
		public class ModelError {
			/// <summary>
			/// Get/sets the sender.
			/// </summary>
			public object Sender { get; internal set; }

			/// <summary>
			/// Gets/sets the error message.
			/// </summary>
			public string Message { get; internal set; }
		}
		#endregion

		#region Members
		/// <summary>
		/// The list of current errors.
		/// </summary>
		public readonly IList<ModelError> Errors = new List<ModelError>();
		#endregion

		/// <summary>
		/// Gets if the current modelstate is valid and has no errors.
		/// </summary>
		public bool IsValid {
			get { return Errors.Count == 0; }
		}

		/// <summary>
		/// Adds a new error to the model state.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="message">The error message</param>
		public void AddError(object sender, string message) {
			Errors.Add(new ModelError() { 
				Sender = sender,
				Message = message
			});
		}
	}
}