using System;

namespace Konorion.TShock4Support
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class FunctionAttribute : Attribute
	{
		public string Name { get; }

		public FunctionAttribute(string name)
		{
			Name = name;
		}
	}
}
