using System.Collections.Immutable;
using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ClosingGate : IDoor
    {
        public bool IsOpen { get; private set; } = true;
        public bool TriedToOpen { get; private set; }

        public bool Open(ImmutableDictionary<Cheat, bool> cheats, Player player)
        {
            TriedToOpen = true;

            if (cheats[Cheat.MoveThroughDoors]) return true;
            
            if (!IsOpen) return IsOpen;
            
            IsOpen = false;
            
            return true;
        }
    }
}