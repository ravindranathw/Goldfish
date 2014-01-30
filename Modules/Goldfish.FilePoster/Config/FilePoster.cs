/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Config
{
	/// <summary>
	/// Config parameters available for the FilePoster module
	/// </summary>
	public static class FilePoster
	{
		/// <summary>
		/// Gets/sets the optional id of the default author.
		/// </summary>
		public static Guid? DefaultAuthorId {
			get { return Utils.GetParam<Guid?>("FILEPOSTER_AUTHOR", s => !String.IsNullOrEmpty(s) ? new Guid?(new Guid(s)) : new Guid?()); }
			set { Utils.SetParam("FILEPOSTER_AUTHOR", value); }
		}
	}
}