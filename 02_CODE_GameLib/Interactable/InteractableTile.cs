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

        /// <summary>
        /// Checks if object can interact with each other.
        ///
        /// Defaults to player because player has the most interactions.
        /// </summary>
        /// <param name="other">The object it wants to interact with</param>
        /// <returns>bool</returns>
        public virtual bool CollidesWith(IInteractable other) => other.X == X && other.Y == Y;

        public virtual bool AllowedToCollideWith(IInteractable other) => other is Player;

        public abstract void InteractWith(IInteractable other);
    }
}