using System;
using System.IO;
using Orion.Framework;
using TShockAPI;

namespace Konorion.Script
{
	[Plugin("Test plugin", Author = "")]
	public sealed class TestPlugin : Plugin
	{
		private const string Folder = "scripts";

		public TestPlugin(Orion.Orion orion) : base(orion)
		{
			var serv = Orion.GetService<IScriptService>();

			serv.Execute("print('Script service is runnning...')");

			Test();

			Directory.CreateDirectory(Folder);
		}

		private void Test()
		{
			Commands.ChatCommands.Add(new Command(x =>
			{
				var serv = Orion.GetService<IScriptService>();
				
				foreach (var file in Directory.EnumerateFiles(Folder, "*.lua"))
				{
					try
					{
						serv.Execute(File.ReadAllText(file));
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
