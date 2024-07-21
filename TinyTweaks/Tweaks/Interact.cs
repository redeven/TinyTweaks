using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using Dalamud.Game.ClientState.Objects.Types;
using TinyTweaks.Utils;
using Dalamud.Game.Command;

namespace TinyTweaks.Tweaks
{
    internal class Interact : ITweak
    {
        private const string CommandName = "/interact";

        public void Enable()
        {
            Svc.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Interacts with target. Same as the Confirm action/key.",
            });
        }

        public void Disable()
        {
            Svc.CommandManager.RemoveHandler(CommandName);
        }

        private unsafe void OnCommand(string command, string args)
        {
            var target = Svc.Targets.Target;
            var player = Svc.ClientState.LocalPlayer;
            if (target != null && player != null)
            {
                if (Vector3.Distance(target.Position, player.Position) < GetValidInteractionDistance(target) && ((GameObject*)target.Address)->GetIsTargetable())
                {
                    TargetSystem.Instance()->InteractWithObject((GameObject*)target.Address);
                }
            }
        }

        private float GetValidInteractionDistance(IGameObject obj)
        {
            return 4.6f;
        }
    }
}
