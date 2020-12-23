using System.Collections.Immutable;
using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public interface IDoor
    {
        public bool IsOpen { get; }
        public bool TriedToOpen { get; }
        public bool Open(ImmutableDictionary<Cheat, bool> cheats, Player player);
    }
}