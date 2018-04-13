using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using Orion.Entities;
using Orion.Framework;
using Orion.Items;
using Orion.Npcs;
using Orion.Players;
using Orion.Projectiles;
using Orion.World;
using LuaScript = MoonSharp.Interpreter.Script;

namespace Konorion.Script
{
    public sealed class LuaScriptService : SharedService, IScriptService
    {
        public object Execute(string code)
        {
            var script = new LuaScript(CoreModules.Preset_Default);
#if DEBUG
            var debugServer = new MoonSharpVsCodeDebugServer();
            debugServer.AttachToScript(script, "random");
#endif
            SetGlobalVariables(script.Globals);

            return script.DoString(code);
        }

        public void DefineVariable(string identifier, object instance)
        {
            if (instance is IService || instance is ISharedService)
            {
                _variables[identifier] = instance;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void SetGlobalVariables(Table globalVariables)
        {
            foreach (var kvp in _variables)
            {
                UserData.RegisterType(kvp.Value.GetType());

                globalVariables[kvp.Key] = kvp.Value;
            }
        }

        private void MapServices()
        {
            var dict = new Dictionary<string, object>
            {
                ["player"] = Orion.GetService<IPlayerService>(),
                ["npc"] = Orion.GetService<INpcService>(),
                ["projectile"] = Orion.GetService<IProjectileService>(),
                ["item"] = Orion.GetService<IItemService>(),
                ["world"] = Orion.GetService<IWorldService>(),
                // ["configuration"] = Orion.GetService<IConfigurationService<GenericConfiguration>>()
            };

            foreach (var kvp in dict)
            {
                DefineVariable(kvp.Key, kvp.Value);
            }
        }

        private static void RegisterOrionTypes()
        {
            UserData.RegisterType<IOrionEntity>();
            UserData.RegisterType<IPlayer>();
            UserData.RegisterType<INpc>();
            UserData.RegisterType<IProjectile>();

            UserData.RegisterType<Vector2>();
            UserData.RegisterType<Player>();
        }

        public LuaScriptService(Orion.Orion orion) : base(orion)
        {
            MapServices();
            RegisterOrionTypes();
        }

        private readonly IDictionary<string, object> _variables = new Dictionary<string, object>();
    }
}
