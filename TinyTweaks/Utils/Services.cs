using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;
using Dalamud.Game.ClientState.Objects;

namespace TinyTweaks.Utils
{
    internal class Svc
    {
        [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
        [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
        [PluginService] internal static IClientState ClientState { get; private set; } = null!;
        [PluginService] internal static IObjectTable Objects { get; private set; } = null!;
        [PluginService] internal static ITargetManager Targets { get; private set; } = null!;
    }
}
