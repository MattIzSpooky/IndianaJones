using CODE_GameLib.Doors;

namespace CODE_GameLib
{
    public class Connection
    {
        public Door Door { get; }
        public WindRose Direction { get; }

        private Room _next;

        public Connection(Room next, WindRose direction)
        {
            _next = next;
            Direction = direction;
        }

        public Connection(Room next, WindRose direction, Door door)
        {
            _next = next;
            Direction = direction;
            Door = door;
        }

        public void GoToNext()
        {
        }
    }
}