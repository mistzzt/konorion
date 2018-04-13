using Orion.Framework;

namespace Konorion.Script
{
	public interface IScriptService : ISharedService
	{
		void Execute(string code);
	}
}
