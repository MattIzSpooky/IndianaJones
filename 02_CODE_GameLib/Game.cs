using System;
using System.Collections.Generic;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private IEnumerable<Room> _rooms;
        private Player _player;

        // TODO: used for render testing. Should be removed later on.
        public WindRose Direction { get; private set; }

        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            _player = player;
        }
        
        public void MovePlayer(WindRose direction)
        {
            Direction = direction;
            Notify(this);
        }
    }
}
