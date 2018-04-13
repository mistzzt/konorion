using System;
using Orion.Framework;

namespace Konorion.Script
{
	[Service("Script", Author = "sbmw")]
    public sealed class ScriptService : SharedService, IScriptService
	{
	    public ScriptService(Orion.Orion orion) : base(orion)
	    {
	    }

	    public void Execute(string code)
	    {
	        Console.WriteLine(code + " ed");
	    }
	}
}
