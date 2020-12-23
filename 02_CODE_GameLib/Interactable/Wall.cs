using System.Collections.Immutable;

namespace CODE_GameLib.Interactable
{
    public class Wall : InteractableTile
    {
        public Wall(Room room, int x, int y) : base(room, x, y)
        {
        }
        public override bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other) => false;
        
        public override void InteractWith(Game context, IInteractable other)
        {
        }
    }
}