using System;
using System.Collections.Generic;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private IEnumerable<Room> _rooms;
        private Player _player;

        public WindRose Direction { get; private set; }

        public void MovePlayer(WindRose direction)
        {
            Direction = direction;
            Notify();
        }
    }
}
