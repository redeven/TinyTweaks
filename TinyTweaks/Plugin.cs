using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin;
using TinyTweaks.Utils;

namespace TinyTweaks;

public sealed class Plugin : IDalamudPlugin
{
    private List<ITweak> Tweaks { get; }

    public Plugin()
    {
        Tweaks = Reflection.ActivateOfInterface<ITweak>().ToList();

        Tweaks.ForEach((tweak) => tweak.Enable());
    }

    public void Dispose()
    {
        Tweaks.ForEach((tweak) => tweak.Disable());
    }
}
