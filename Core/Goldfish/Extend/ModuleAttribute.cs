using System;

namespace Goldfish.Extend
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ModuleAttribute : Attribute
	{
		public string Name { get; set; }
		public Type Config { get; set; }
	}
}