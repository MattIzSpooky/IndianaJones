﻿using System.Collections.Generic;
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

        public WindRose GetDirectionByRoom(int roomId)
        {
            return _directions.FirstOrDefault(x => x.Value != roomId).Key; // TODO: Bluegh
        }

        public int GetNextRoomId(WindRose windRose, int roomId)
        {
            if (!_directions.ContainsKey(windRose))
                return 0;

            return _directions[windRose] == roomId ? 0 : _directions[windRose];
        }
    }
}