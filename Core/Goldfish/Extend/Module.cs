using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Goldfish.Extend
{
	public abstract class Module : IModule
	{
		public virtual string Name {
			get {
				var attr = this.GetType().GetCustomAttribute<ModuleAttribute>();
				if (attr != null)
					return attr.Name;
				else return this.GetType().Name;
			}
		}
		public bool HasConfig { 
			get {
				return ConfigType != null;
			}
		}
		public Type ConfigType { 
			get {
				var attr = this.GetType().GetCustomAttribute<ModuleAttribute>();
				if (attr != null)
					return attr.Config;
				return null;
			}
		}

		public virtual void Init() { }
	}
}