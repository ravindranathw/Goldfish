/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Web.Routing;
using System.Web.Mvc;

namespace Goldfish.Web.Mvc
{
	/// <summary>
	/// Controller factory using TinyIoC.
	/// </summary>
	public sealed class ControllerFactory : DefaultControllerFactory
	{
		/// <summary>
		/// Gets the requested controller instance.
		/// </summary>
		/// <param name="requestContext">The current request context</param>
		/// <param name="controllerType">The requested controller type</param>
		/// <returns>The controller</returns>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			return (IController)App.Instance.IoCContainer.Resolve(controllerType);
		}
	}
}