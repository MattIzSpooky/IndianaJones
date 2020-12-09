using System;
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
            if (Player.X >= CurrentRoom.Width - 1)
                Player.Move(WindRose.West);
            else if (Player.Y >= CurrentRoom.Height - 1)
                Player.Move(WindRose.North);
            else if (Player.X <= 0)
                Player.Move(WindRose.East);
            else if (Player.Y <= 0)
                Player.Move(WindRose.South);
            else
                Player.Move(direction);

            CheckCollides();
            Notify(this);
        }

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.InteractableTiles)
            {
                if (interactableTile.X == Player.X && interactableTile.Y == Player.Y)
                {
                    interactableTile.InteractWith(Player);
                }
            }
        }
    }
}