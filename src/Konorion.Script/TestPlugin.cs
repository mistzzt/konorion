using Orion.Framework;

namespace Konorion.Script
{
	[Plugin("Test plugin", Author = "")]
	public class TestPlugin : Plugin
	{
		public TestPlugin(Orion.Orion orion) : base(orion)
		{
			var serv = Orion.GetService<IScriptService>();

			serv.Execute("code");
		}

		
	}
}
