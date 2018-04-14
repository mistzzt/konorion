using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using Orion.Framework;
using LuaScript = MoonSharp.Interpreter.Script;

namespace Konorion.Script
{
	public sealed class LuaScriptService : SharedService, IScriptService
	{
		private static readonly Regex ServiceName = new Regex(@"I(\w+)(Service){1}", RegexOptions.Compiled);

		private void Initialize()
		{
			if (_initialized)
			{
				return;
			}

			// register all services
			foreach (var service in Orion.GetServices<ISharedService>())
			{
				UserData.RegisterType(service.GetType());

				// put into global variables
				foreach (var type in service.GetType().GetInterfaces())
				{
					if (type == typeof(ISharedService))
					{
						continue;
					}

					var match = ServiceName.Match(type.Name);
					if (!match.Success)
					{
						continue;
					}

					var name = match.Groups[1].Value.ToLowerInvariant();
					DefineVariable(name, service);
				}
			}

			// register all plugins
			foreach (var service in Orion.GetServices<Plugin>())
			{
				UserData.RegisterType(service.GetType());
			}

			// register basic types in XNA
			foreach (var type in typeof(Microsoft.Xna.Framework.Color).Assembly.GetExportedTypes())
			{
				UserData.RegisterType(type);
			}

			// register public types in orion
			foreach (var type in typeof(Orion.Orion).Assembly.GetExportedTypes())
			{
				UserData.RegisterType(type);
			}

			_initialized = true;
		}

		public object DoFile(string path)
		{
			Initialize();

			var script = new LuaScript(CoreModules.Preset_Default);
#if DEBUG
			var debugServer = new MoonSharpVsCodeDebugServer();
			debugServer.AttachToScript(script, "random");
#endif
			SetGlobalVariables(script.Globals);

			return ToDotNetType(script.DoFile(path));
		}

		public object DoStream(Stream stream)
		{
			throw new NotImplementedException();
		}

		public object DoString(string code)
		{
			Initialize();

			var script = new LuaScript(CoreModules.Preset_Default);
#if DEBUG
			var debugServer = new MoonSharpVsCodeDebugServer();
			debugServer.AttachToScript(script, "random");
#endif
			SetGlobalVariables(script.Globals);

			return ToDotNetType(script.DoString(code));
		}

		public void DefineVariable(string identifier, object instance)
		{
			if (_variables.Any(x => x.Key.Equals(identifier, StringComparison.OrdinalIgnoreCase)))
			{
				throw new InvalidOperationException();
			}

			_variables.Add(new KeyValuePair<string, object>(identifier, instance));
		}

		private void SetGlobalVariables(Table globalVariables)
		{
			foreach (var kvp in _variables)
			{
				globalVariables[kvp.Key] = kvp.Value;
			}
		}

		public LuaScriptService(Orion.Orion orion) : base(orion)
		{
		}

		private static object ToDotNetType(DynValue value)
		{
			switch (value.Type)
			{
				case DataType.Nil:
				case DataType.Void:
					return null;
				case DataType.Boolean:
					return value.Boolean;
				case DataType.Number:
					return value.Number;
				case DataType.String:
					return value.String;
				case DataType.Function:
				case DataType.Table:
				case DataType.Tuple:
				case DataType.UserData:
				case DataType.Thread:
				case DataType.ClrFunction:
				case DataType.TailCallRequest:
				case DataType.YieldRequest:
					throw new NotSupportedException();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private bool _initialized;

		private readonly ISet<KeyValuePair<string, object>> _variables = new HashSet<KeyValuePair<string, object>>();
	}
}
