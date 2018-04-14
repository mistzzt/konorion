using System;
using System.IO;
using Orion.Framework;
using TShockAPI;

namespace Konorion.Script
{
	[Plugin("Test plugin", Author = "")]
	public sealed class TestPlugin : Plugin
	{
		private const string ScriptPath = "scripts";

		public TestPlugin(Orion.Orion orion) : base(orion)
		{
			Test();

			Directory.CreateDirectory(ScriptPath);
		}

		private void Test()
		{
			Commands.ChatCommands.Add(new Command(x =>
			{
				var serv = Orion.GetService<IScriptService>();

				foreach (var file in Directory.EnumerateFiles(ScriptPath, "*.lua"))
				{
					try
					{
						serv.DoFile(file);
						x.Player.SendInfoMessage(file);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}

				x.Player.SendInfoMessage("ok");
			}, "test2"));
		}
	}
}
