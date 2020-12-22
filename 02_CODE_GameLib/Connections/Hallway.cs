#nullable enable
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib.Connections
{
    public class Hallway
    {
        public IDoor? Door { get; }

        private readonly Dictionary<Direction, Room> _directions;

        public Hallway(Dictionary<Direction, Room> direction, IDoor door)
        {
            _directions = direction;
            Door = door;
        }

        public bool BelongsToRoom(int roomId) => _directions.Any(d => d.Value.Id == roomId);

        public Direction GetDirectionByRoom(int roomId) =>
            _directions.FirstOrDefault(x => x.Value.Id != roomId).Key;

        public Room? GetNextRoom(Direction direction, int roomId)
        {
            if (!_directions.ContainsKey(direction)) return null;

            return _directions[direction].Id == roomId ? null : _directions[direction];
        }
    }
}