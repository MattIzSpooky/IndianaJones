namespace CODE_GameLib.Interactable
{
    public abstract class BaseInteractable : IPosition
    {
        public int X { get; }
        public int Y { get; }

        protected Room room;

        protected BaseInteractable(Room room, int x, int y)
        {
            room = room;
            X = x;
            Y = y;
        }
        public abstract void InteractWith(Player player);
    }
}