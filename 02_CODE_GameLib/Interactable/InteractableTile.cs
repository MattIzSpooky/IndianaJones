namespace CODE_GameLib.Interactable
{
    public abstract class InteractableTile : IInteractable, IPosition
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

        public abstract void InteractWith(Player player);
        public new abstract string GetType();
    }
}