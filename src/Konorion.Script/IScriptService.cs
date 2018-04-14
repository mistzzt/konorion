using System.IO;
using Orion.Framework;

namespace Konorion.Script
{
	public interface IScriptService : ISharedService
	{
		object DoFile(string path);

		object DoStream(Stream stream);

		object DoString(string code);

		void DefineVariable(string identifier, object instance);
	}
}
