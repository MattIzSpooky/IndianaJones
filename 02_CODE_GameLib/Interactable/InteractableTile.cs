using System.Collections.Immutable;

namespace CODE_GameLib.Interactable
{
    /// <summary>
    /// An interactable that has a static position in a room
    /// </summary>
    public abstract class InteractableTile : IInteractable
    {
        public int X { get; }
        public int Y { get; }
        protected Room Room { get; }

        protected InteractableTile(Room room, int x, int y)
        {
            X = x;
            Y = y;
            Room = room;
        }
        
        public virtual bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other) => other is Player;

        public abstract void InteractWith(Game gameContext, IInteractable other);
    }
}