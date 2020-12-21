#nullable enable
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib
{
    public class Hallway
    {
        public DoorContext? DoorContext { get; }

        private readonly Dictionary<Direction, int> _directions;

        public Hallway(Dictionary<Direction, int> direction, DoorContext doorContext)
        {
            _directions = direction;
            DoorContext = doorContext;
        }

        public bool BelongsToRoom(int roomId) => _directions.Any(d => d.Value == roomId);

        public Direction GetDirectionByRoom(int roomId) =>
            _directions.FirstOrDefault(x => x.Value != roomId).Key;

        public int GetNextRoomId(Direction direction, int roomId)
        {
            if (!_directions.ContainsKey(direction))
                return 0;

            return _directions[direction] == roomId ? 0 : _directions[direction];
        }
    }
}