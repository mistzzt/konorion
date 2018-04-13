using System;
using Orion.Framework;

using OrionPlugin = Orion.Framework.Plugin;
using OrionInstance = Orion.Orion;

namespace Konorion.Plugin
{
	public sealed class OrionContainer
	{
		public static OrionInstance Orion => Instance._initialized ? Instance._orion : throw new InvalidOperationException();

		internal void Initialize()
	    {
		    foreach (var service in _orion.GetServices<ISharedService>())
		    {
			    Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
		    }
		    foreach (var plugin in _orion.GetServices<OrionPlugin>())
		    {
			    Console.WriteLine($"  * Loading {plugin.Name} by {plugin.Author}");
		    }

		    _initialized = true;
	    }

		private OrionContainer()
		{
			_instance = this;

			_orion = new OrionInstance();
		}

		private bool _initialized;
		private readonly OrionInstance _orion;

		private static OrionContainer _instance;
		public static OrionContainer Instance => _instance ?? (_instance = new OrionContainer());
	}
}
