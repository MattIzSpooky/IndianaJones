using System.Collections.Generic;
using System.Linq;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private IEnumerable<Room> _rooms;
        public Player Player { get; }
        
        public Room CurrentRoom { get; }

        // TODO: used for render testing. Should be removed later on.
        public WindRose Direction { get; private set; }

        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            Player = player;

            CurrentRoom = _rooms.First(e => e.Player != null);
            
            Notify(this);
        }
        
        public void MovePlayer(WindRose direction)
        {
            Player.Move(direction);
            Notify(this);
        }
    }
}
