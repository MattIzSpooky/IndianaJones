using System.Collections.Immutable;
using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : IDoor
    {
        public bool IsOpen { get; private set; }
        public bool TriedToOpen { get; private set; }
        public bool Open(ImmutableDictionary<Cheat, bool> cheats, Player player)
        {
            TriedToOpen = true;
            
            return cheats[Cheat.MoveThroughDoors] || IsOpen;
        }

        public void Toggle() => IsOpen = !IsOpen;
    }
}