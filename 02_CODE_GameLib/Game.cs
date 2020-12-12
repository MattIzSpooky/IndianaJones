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

        private readonly int _stonesNeeded;


        public Game(IEnumerable<Room> rooms, Player player, int stones)
        {
            _rooms = rooms;
            Player = player;
            _stonesNeeded = stones;

            CurrentRoom = _rooms.First(e => e.Player != null);
        }

        public void MovePlayer(WindRose direction)
        {
            Player.TryMove(direction);

            CheckCollides();
            CheckGameEnd();

            CheckConnections(direction);

            if (!Player.CanMove) Player.RevertMove(direction);

            Notify(this);
        }

        private void CheckConnections(WindRose direction)
        {
            if (Player.X <= CurrentRoom.Width &&
                Player.Y <= CurrentRoom.Height &&
                Player.X >= 0 &&
                Player.Y >= 0) return;

            var nextRoomId = CurrentRoom.Leave(direction);
            if (nextRoomId == 0) return;

            var hallway = CurrentRoom.GetHallWayByDirection(direction);

            if (hallway.DoorContext == null) PlayerEnterRoom(direction, nextRoomId);
            else if (hallway.DoorContext.Open(Player)) PlayerEnterRoom(direction, nextRoomId);
            else Player.CanMove = false;
        }

        private void PlayerEnterRoom(WindRose direction, int nextRoomId)
        {
            CurrentRoom = _rooms.First(r => r.Id == nextRoomId);
            Player.EnterRoom(CurrentRoom, direction);
        }

        private void CheckGameEnd() => HasEnded = Player.Score == _stonesNeeded || Player.Lives <= 0;

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.InteractableTiles)
            {
                if (interactableTile.CanInteractWith(Player)) interactableTile.InteractWith(Player);
            }
        }
    }
}