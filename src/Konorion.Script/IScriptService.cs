using Orion.Framework;

namespace Konorion.Script
{
    public interface IScriptService : ISharedService
    {
        object Execute(string code);

        void DefineVariable(string identifier, object instance);
    }
}
