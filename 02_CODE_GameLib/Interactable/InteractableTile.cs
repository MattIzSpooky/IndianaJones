using System.Collections.Immutable;

namespace CODE_GameLib.Interactable
{
    /// <summary>
    /// An interactable that has a static position in a room
    /// </summary>
    public abstract class InteractableTile : IInteractable
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        protected Room Room { get; }

        protected InteractableTile(Room room, int x, int y)
        {
            X = x;
            Y = y;
            Room = room;
        }
        
        public virtual bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other) => other is Player;

        public abstract void InteractWith(Game context, IInteractable other);
    }
}