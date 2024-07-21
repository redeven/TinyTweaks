using System;
using System.Linq;
using System.Numerics;
using TinyTweaks.Utils;
using Dalamud.Game.Command;
using Dalamud.Game.ClientState.Objects.Types;
using GameObjectStruct = FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject;

namespace TinyTweaks.Tweaks
{
    internal class BetterTarget : ITweak
    {
        private const string CommandName = "/btarget";

        public void Enable()
        {
            Svc.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Allows targetting closest entity that matches any term of a space-delimited list",
            });
        }

        public void Disable()
        {
            Svc.CommandManager.RemoveHandler(CommandName);
        }

        private unsafe void OnCommand(string command, string args)
        {
            var searchTerms = args.Split(" ");

            IGameObject? closestMatch = null;
            var closestDistance = float.MaxValue;
            var player = Svc.ClientState.LocalPlayer;
            if (player == null) return;
            foreach (var actor in Svc.Objects)
            {
                if (actor == null) continue;
                var valueFound = searchTerms.Any(searchTerm => actor.Name.TextValue.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
                if (valueFound && ((GameObjectStruct*)actor.Address)->GetIsTargetable())
                {
                    var distance = Vector3.Distance(player.Position, actor.Position);
                    if (closestMatch == null)
                    {
                        closestMatch = actor;
                        closestDistance = distance;
                        continue;
                    }

                    if (closestDistance > distance)
                    {
                        closestMatch = actor;
                        closestDistance = distance;
                    }
                }
            }
            if (closestMatch != null)
            {
                Svc.Targets.Target = closestMatch;
            }
        }
    }
}
