using System.Collections.Generic;
using System.Linq;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private readonly IEnumerable<Room> _rooms;
        public Player Player { get; }
        public Room CurrentRoom { get; private set; }
        public bool HasEnded { get; private set; }

        private const int StonesNeeded = 5;


        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            Player = player;

            CurrentRoom = _rooms.First(e => e.Player != null);
        }

        public void MovePlayer(WindRose direction)
        {
            Player.TryMove(direction);

            CheckCollides();
            CheckConnections(direction);
            
            CheckGameEnd();

            if (!Player.CanMove) Player.RevertMove(direction);

            Notify(this);
        }

        private void CheckConnections(WindRose direction)
        {
            if (Player.X != CurrentRoom.Width && Player.Y != CurrentRoom.Height) return;
            
            var nextRoomId = CurrentRoom.Enter(direction);
            CurrentRoom = _rooms.First(r => r.Id == nextRoomId);
            Player.EnterRoom(CurrentRoom.Width, CurrentRoom.Height);
        }

        private void CheckGameEnd() => HasEnded = Player.Score == StonesNeeded || Player.Lives <= 0;

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.InteractableTiles)
            {
                if (interactableTile.CanInteractWith(Player)) interactableTile.InteractWith(Player);
            }
        }
    }
}