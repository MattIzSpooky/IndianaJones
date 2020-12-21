using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Interactable;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private readonly IEnumerable<Room> _rooms;
        public Player Player { get; }
        public Room CurrentRoom { get; private set; }
        public bool HasEnded { get; private set; }
        public int Stones { get; }

        public Game(IEnumerable<Room> rooms, Player player, int stones)
        {
            _rooms = rooms;
            Player = player;
            Stones = stones;

            CurrentRoom = _rooms.First(e => e.Player != null);
        }

        public void MovePlayer(Direction direction)
        {
            Player.TryMove(CurrentRoom, direction);

            CheckCollides();
            CheckGameEnd();

            CheckHallwaysEntered(direction);

            Notify(this);
        }

        private void CheckHallwaysEntered(Direction direction)
        {
            if (Player.X <= CurrentRoom.Width &&
                Player.Y <= CurrentRoom.Height &&
                Player.X >= 0 &&
                Player.Y >= 0) return;

            var nextRoomId = CurrentRoom.Leave(direction);
            if (nextRoomId == 0) return;

            var hallway = CurrentRoom.GetHallWayByDirection(direction);

            if (hallway.Door == null) PlayerEnterRoom(direction, nextRoomId);
            else if (hallway.Door.Open(Player)) PlayerEnterRoom(direction, nextRoomId);
        }

        private void PlayerEnterRoom(Direction direction, int nextRoomId)
        {
            CurrentRoom = _rooms.First(r => r.Id == nextRoomId);
            Player.EnterRoom(CurrentRoom, direction);
        }

        private void CheckGameEnd() => HasEnded = Player.Score == Stones || Player.Lives <= 0;

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.Interactables)
            {
                if (interactableTile.AllowedToCollideWith(Player) && interactableTile.CollidesWith(Player)) 
                    interactableTile.InteractWith(Player);
            }
        }
    }
}