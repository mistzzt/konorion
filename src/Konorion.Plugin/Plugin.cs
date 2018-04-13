using System;
using Terraria;
using TerrariaApi.Server;

namespace Konorion.Plugin
{
    [ApiVersion(2, 1)]
    public sealed class Plugin : TerrariaPlugin
    {
        public override void Initialize()
        {
            OrionHooksHelper.PrepareHooksAndInitializeOrion();
        }

        public Plugin(Main game) : base(game)
        {
        }

        public override string Name => GetType().Namespace;
        public override string Author => "mist";
        public override Version Version => GetType().Assembly.GetName().Version;
    }
}
