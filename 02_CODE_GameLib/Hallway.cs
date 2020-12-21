#nullable enable
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib
{
    public class Hallway
    {
        public IDoor? Door { get; }

        private readonly Dictionary<Direction, int> _directions;

        public Hallway(Dictionary<Direction, int> direction, IDoor door)
        {
            _directions = direction;
            Door = door;
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