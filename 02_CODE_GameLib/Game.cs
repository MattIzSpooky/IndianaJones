using System.Collections.Generic;
using System.Linq;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private IEnumerable<Room> _rooms;
        private Player _player;
        
        public Room CurrentRoom { get; }

        // TODO: used for render testing. Should be removed later on.
        public WindRose Direction { get; private set; }

        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            _player = player;

            CurrentRoom = _rooms.First(e => e.Player != null);
        }
        
        public void MovePlayer(WindRose direction)
        {
            Direction = direction;
            Notify(this);
        }
    }
}
