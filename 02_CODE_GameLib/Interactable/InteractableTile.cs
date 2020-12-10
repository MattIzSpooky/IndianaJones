namespace CODE_GameLib.Interactable
{
    public abstract class InteractableTile : IInteractable
    {
        public int X { get; }
        public int Y { get; }
        protected Room Room { get; }
        public Tile Tile { get; protected set; }

        protected InteractableTile(Room room, int x, int y, Tile tile)
        {
            X = x;
            Y = y;
            Room = room;
            Tile = tile;
        }

        public virtual bool CanInteractWith(IInteractable other) => other is Player player && player.X == X && player.Y == Y;
        public abstract void InteractWith(IInteractable other);
    }
}