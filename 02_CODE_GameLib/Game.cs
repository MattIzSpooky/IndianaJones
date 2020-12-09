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

        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            Player = player;

            CurrentRoom = _rooms.First(e => e.Player != null);
            
            Notify(this);
        }
        
        public void MovePlayer(WindRose direction)
        {
            if (Player.X >= CurrentRoom.Width - 1) return;
            // if (Player.X <= CurrentRoom.Width + 1) return;
            
            // if (Player.X >= CurrentRoom.Width - 1) return;
            // if (Player.X >= CurrentRoom.Width - 1) return;
            
            Player.Move(direction);
            Notify(this);
        }
    }
}
