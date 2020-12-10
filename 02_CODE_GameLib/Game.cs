using System.Collections.Generic;
using System.Linq;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        private readonly IEnumerable<Room> _rooms;
        public Player Player { get; }
        public Room CurrentRoom { get; }
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
            /*if (Player.X >= CurrentRoom.Width - 1)
                Player.TryMove(WindRose.West);
            else if (Player.Y >= CurrentRoom.Height - 1)
                Player.TryMove(WindRose.North);
            else if (Player.X <= 0)
                Player.TryMove(WindRose.East);
            else if (Player.Y <= 0)
                Player.TryMove(WindRose.South);
            else
                Player.TryMove(direction);*/

            Player.TryMove(direction);

            CheckCollides();
            CheckGameEnd();

            if (!Player.CanMove) Player.RevertMove(direction);

            Notify(this);
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