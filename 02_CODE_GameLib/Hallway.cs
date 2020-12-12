#nullable enable
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Interactable.Doors;

namespace CODE_GameLib
{
    public class Hallway
    {
        public DoorContext? DoorContext { get; }

        private readonly Dictionary<WindRose, int> _directions;

        public Hallway(Dictionary<WindRose, int> direction, DoorContext doorContext)
        {
            _directions = direction;
            DoorContext = doorContext;
        }

        public bool BelongsToRoom(int roomId) => _directions.Any(d => d.Value == roomId);

        public WindRose GetDirectionByRoom(int roomId) =>
            _directions.FirstOrDefault(x => x.Value != roomId).Key;

        public int GetNextRoomId(WindRose windRose, int roomId)
        {
            if (!_directions.ContainsKey(windRose))
                return 0;

            return _directions[windRose] == roomId ? 0 : _directions[windRose];
        }
    }
}