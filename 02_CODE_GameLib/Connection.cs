using System.Collections.Generic;
using CODE_GameLib.Interactable.Doors;

namespace CODE_GameLib
{
    public class Connection
    {
        public Door Door { get; }

        private readonly Dictionary<WindRose, int> _directions;

        public Connection(Dictionary<WindRose,int>  direction)
        {
            _directions = direction;
        }

        public Connection(Dictionary<WindRose, int> direction, Door door) : this(direction)
        {
            Door = door;
        }

        public bool BelongsToRoom(int roomId)
        {
            return true;
        }

        public void GoToNext()
        {
        }
    }
}