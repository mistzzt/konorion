using OTAPI;
using TerrariaApi.Server;

namespace Konorion.Plugin
{
    internal static class OrionHooksHelper
    {
        internal static void PrepareHooksAndInitializeOrion()
        {
            OrionContainer.Instance.Initialize();

            var preUpdate = Hooks.Game.PreUpdate;
            var postUpdate = Hooks.Game.PostUpdate;
            var hardmodeTileUpdate = Hooks.World.HardmodeTileUpdate;
            var preInitialize = Hooks.Game.PreInitialize;
            var started = Hooks.Game.Started;
            var statue = Hooks.World.Statue;

            var preSetDefaultsById = Hooks.Item.PreSetDefaultsById;
            var preNetDefaults = Hooks.Item.PreNetDefaults;
            var quickStack = Hooks.Chest.QuickStack;

            var preSetDefaultsByIdNpc = Hooks.Npc.PreSetDefaultsById;
            var preNetDefaultsNpc = Hooks.Npc.PreNetDefaults;
            var strike = Hooks.Npc.Strike;
            var preTransform = Hooks.Npc.PreTransform;
            var spawn = Hooks.Npc.Spawn;
            var preDropLoot = Hooks.Npc.PreDropLoot;
            var bossBagItem = Hooks.Npc.BossBagItem;
            var preAi = Hooks.Npc.PreAI;
            var killed = Hooks.Npc.Killed;

            var postSetDefaultsById = Hooks.Projectile.PostSetDefaultsById;
            var preAiProjectile = Hooks.Projectile.PreAI;

            var startCommandThread = Hooks.Command.StartCommandThread;
            var process = Hooks.Command.Process;
            var preReset = Hooks.Net.RemoteClient.PreReset;

            var announcementBox = Hooks.Wiring.AnnouncementBox;

            var pressurePlate = Hooks.Collision.PressurePlate;
            var preSaveWorld = Hooks.World.IO.PreSaveWorld;
            var preHardmode = Hooks.World.PreHardmode;
            var dropMeteor = Hooks.World.DropMeteor;
            var christmas = Hooks.Game.Christmas;
            var halloween = Hooks.Game.Halloween;
            var spreadGrass = Hooks.World.SpreadGrass;

            ServerApi.Hooks.AttachOTAPIHooks(new string[0]);

            Hooks.Game.PreUpdate += preUpdate;
            Hooks.Game.PostUpdate += postUpdate;
            Hooks.World.HardmodeTileUpdate += hardmodeTileUpdate;
            Hooks.Game.PreInitialize += preInitialize;
            Hooks.Game.Started += started;
            Hooks.World.Statue += statue;

            Hooks.Item.PreSetDefaultsById += preSetDefaultsById;
            Hooks.Item.PreNetDefaults += preNetDefaults;
            Hooks.Chest.QuickStack += quickStack;

            Hooks.Npc.PreSetDefaultsById += preSetDefaultsByIdNpc;
            Hooks.Npc.PreNetDefaults += preNetDefaultsNpc;
            Hooks.Npc.Strike += strike;
            Hooks.Npc.PreTransform += preTransform;
            Hooks.Npc.Spawn += spawn;
            Hooks.Npc.PreDropLoot += preDropLoot;
            Hooks.Npc.BossBagItem += bossBagItem;
            Hooks.Npc.PreAI += preAi;
            Hooks.Npc.Killed += killed;

            Hooks.Projectile.PostSetDefaultsById += postSetDefaultsById;
            Hooks.Projectile.PreAI += preAiProjectile;

            Hooks.Command.StartCommandThread += startCommandThread;
            Hooks.Command.Process += process;
            Hooks.Net.RemoteClient.PreReset += preReset;

            Hooks.Wiring.AnnouncementBox += announcementBox;

            Hooks.Collision.PressurePlate += pressurePlate;
            Hooks.World.IO.PreSaveWorld += preSaveWorld;
            Hooks.World.PreHardmode += preHardmode;
            Hooks.World.DropMeteor += dropMeteor;
            Hooks.Game.Christmas += christmas;
            Hooks.Game.Halloween += halloween;
            Hooks.World.SpreadGrass += spreadGrass;
        }

#if OLD
		internal static void PrepareHooksAndInitializeOrion()
		{
			var preUpdate = Hooks.Game.PreUpdate;
			var postUpdate = Hooks.Game.PostUpdate;
			var hardmodeTileUpdate = Hooks.World.HardmodeTileUpdate;
			var preInitialize = Hooks.Game.PreInitialize;
			var started = Hooks.Game.Started;
			var statue = Hooks.World.Statue;

			var preSetDefaultsById = Hooks.Item.PreSetDefaultsById;
			var preNetDefaults = Hooks.Item.PreNetDefaults;
			var quickStack = Hooks.Chest.QuickStack;

			var sendData = Hooks.Net.SendData;
			var receiveData = Hooks.Net.ReceiveData;
			var preGreet = Hooks.Player.PreGreet;
			var sendBytes = Hooks.Net.SendBytes;
			var accepted = Hooks.Net.Socket.Accepted;
			// var beforeBroadcastChatMessage = Hooks.Net.BeforeBroadcastChatMessage;
			var nameCollision = Hooks.Player.NameCollision;

			var preSetDefaultsByIdNpc = Hooks.Npc.PreSetDefaultsById;
			var preNetDefaultsNpc = Hooks.Npc.PreNetDefaults;
			var strike = Hooks.Npc.Strike;
			var preTransform = Hooks.Npc.PreTransform;
			var spawn = Hooks.Npc.Spawn;
			var preDropLoot = Hooks.Npc.PreDropLoot;
			var bossBagItem = Hooks.Npc.BossBagItem;
			var preAi = Hooks.Npc.PreAI;
			var killed = Hooks.Npc.Killed;

			var postSetDefaultsById = Hooks.Projectile.PostSetDefaultsById;
			var preAiProjectile = Hooks.Projectile.PreAI;

			var startCommandThread = Hooks.Command.StartCommandThread;
			var process = Hooks.Command.Process;
			var preReset = Hooks.Net.RemoteClient.PreReset;

			var announcementBox = Hooks.Wiring.AnnouncementBox;

			var pressurePlate = Hooks.Collision.PressurePlate;
			var preSaveWorld = Hooks.World.IO.PreSaveWorld;
			var preHardmode = Hooks.World.PreHardmode;
			var dropMeteor = Hooks.World.DropMeteor;
			var christmas = Hooks.Game.Christmas;
			var halloween = Hooks.Game.Halloween;
			var spreadGrass = Hooks.World.SpreadGrass;

			OrionContainer.Instance.Initialize();

			Hooks.Game.PreUpdate += preUpdate;
			Hooks.Game.PostUpdate += postUpdate;
			Hooks.World.HardmodeTileUpdate += hardmodeTileUpdate;
			Hooks.Game.PreInitialize += preInitialize;
			Hooks.Game.Started += started;
			Hooks.World.Statue += statue;

			Hooks.Item.PreSetDefaultsById += preSetDefaultsById;
			Hooks.Item.PreNetDefaults += preNetDefaults;
			Hooks.Chest.QuickStack += quickStack;

			Hooks.Net.SendData += sendData;
			Hooks.Net.ReceiveData += receiveData;
			Hooks.Player.PreGreet += preGreet;
			Hooks.Net.SendBytes += sendBytes;
			Hooks.Net.Socket.Accepted += accepted;
			// Hooks.Net.BeforeBroadcastChatMessage += beforeBroadcastChatMessage;
			Hooks.Player.NameCollision += nameCollision;

			Hooks.Npc.PreSetDefaultsById += preSetDefaultsByIdNpc;
			Hooks.Npc.PreNetDefaults += preNetDefaultsNpc;
			Hooks.Npc.Strike += strike;
			Hooks.Npc.PreTransform += preTransform;
			Hooks.Npc.Spawn += spawn;
			Hooks.Npc.PreDropLoot += preDropLoot;
			Hooks.Npc.BossBagItem += bossBagItem;
			Hooks.Npc.PreAI += preAi;
			Hooks.Npc.Killed += killed;

			Hooks.Projectile.PostSetDefaultsById += postSetDefaultsById;
			Hooks.Projectile.PreAI += preAiProjectile;

			Hooks.Command.StartCommandThread += startCommandThread;
			Hooks.Command.Process += process;
			Hooks.Net.RemoteClient.PreReset += preReset;

			Hooks.Wiring.AnnouncementBox += announcementBox;

			Hooks.Collision.PressurePlate += pressurePlate;
			Hooks.World.IO.PreSaveWorld += preSaveWorld;
			Hooks.World.PreHardmode += preHardmode;
			Hooks.World.DropMeteor += dropMeteor;
			Hooks.Game.Christmas += christmas;
			Hooks.Game.Halloween += halloween;
			Hooks.World.SpreadGrass += spreadGrass;
		}
#endif
    }
}
