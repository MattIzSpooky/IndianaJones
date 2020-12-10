using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Interactable.Doors;

namespace CODE_GameLib
{
    public class Connection
    {
        public Door Door { get; }

        private readonly Dictionary<WindRose, int> _directions;

        public Connection(Dictionary<WindRose, int> direction)
        {
            _directions = direction;
        }

        public Connection(Dictionary<WindRose, int> direction, Door door) : this(direction)
        {
            Door = door;
        }

        public bool BelongsToRoom(int roomId)
        {
            return _directions.Any(d => d.Value == roomId);
        }

        public int GetNextRoomId(WindRose windRose, int roomId)
        {
            //_directions.Where(d => d.Key == windRose)
            return _directions[windRose];
        }
    }
}