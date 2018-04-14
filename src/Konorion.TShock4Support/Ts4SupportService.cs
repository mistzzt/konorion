using System;
using System.Linq;
using System.Reflection;
using Konorion.Script;
using Konorion.TShock4Support.Library;
using Orion.Framework;

namespace Konorion.TShock4Support
{
	[Service("Konorion.TShock4Support", Author = "mist")]
	public sealed class Ts4SupportService : SharedService, ITs4SupportService
	{
		private readonly IScriptService _service;
		
		public Ts4SupportService(Orion.Orion orion) : base(orion)
		{
			_service = Orion.GetService<IScriptService>();

			LoadLibraries();
		}

		private void LoadLibraries()
		{
			var types = typeof(Ts4SupportService).Assembly.ExportedTypes.Where(x =>
				x.GetCustomAttribute<LibraryAttribute>() != null);

			var methods = types.SelectMany(x => x.GetMethods());

			foreach (var method in methods)
			{
				var attr = method.GetCustomAttribute<FunctionAttribute>();
				if (attr == null) continue;

				var obj = BuildDelegate(method, new Commands());
				_service.DefineVariable(attr.Name, obj);
			}
		}

		public static object BuildDelegate(MethodInfo method, object instance)
		{
			var hasReturnValue = method.ReturnType != typeof(void);
			var types = method.GetParameters().Select(x => x.ParameterType).ToList();

			if (hasReturnValue)
			{
				types.Add(method.ReturnType);
			}

			const string funcPrefix = "Func`";
			const string actionPrefix = "Action`";

			var typeName = (hasReturnValue ? funcPrefix : actionPrefix) + types.Count;
			var delegateType = typeof(Func<>).Assembly.GetTypes().Single(x => x.Name == typeName);

			delegateType = delegateType.MakeGenericType(types.ToArray());
			return method.CreateDelegate(delegateType, instance);
		}
	}
}
