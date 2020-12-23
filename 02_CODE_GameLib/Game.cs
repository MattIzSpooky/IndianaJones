using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CODE_GameLib.Interactable;
using Utils;

namespace CODE_GameLib
{
    public class Game : Observable<Game>
    {
        public Player Player { get; }
        public Room CurrentRoom { get; private set; }
        public bool HasEnded { get; private set; }
        public int Stones { get; }
        public ImmutableDictionary<Cheat, bool> Cheats => _cheats.ToImmutableDictionary();
        private readonly Dictionary<Cheat, bool> _cheats;

        public Game(IEnumerable<Room> rooms, Player player, int stones)
        {
            Player = player;
            Stones = stones;

            CurrentRoom = rooms.First(e => e.Player != null);
            _cheats = CreateCheatsDictionary();
        }

        private static Dictionary<Cheat, bool> CreateCheatsDictionary() => Enum.GetValues(typeof(Cheat))
            .Cast<Cheat>()
            .ToDictionary(cheat => cheat, _ => false);

        public void MovePlayer(Direction direction)
        {
            if (Player.AttemptMove(CurrentRoom, direction, Cheats))
            {
                CurrentRoom.MoveMovables();
                CheckCollides();
                CheckGameEnd();
            }

            Notify(this);
        }

        public void ToggleCheat(Cheat cheat)
        {
            _cheats[cheat] = !_cheats[cheat];
            Notify(this);
        }

        public void PlayerEnterRoom(Room room, int x, int y)
        {
            CurrentRoom = room;
            Player.EnterRoom(room, x, y);
        }

        public void PlayerEnterRoom(Direction direction, Room room)
        {
            CurrentRoom = room;
            Player.EnterRoom(CurrentRoom, direction);
        }

        private void CheckGameEnd() => HasEnded = Player.Score == Stones || Player.NumberOfLives <= 0;

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.Interactables)
            {
                if (interactableTile.AllowedToCollideWith(Cheats, Player) && interactableTile.CollidesWith(Player))
                    interactableTile.InteractWith(this, Player);
            }
        }
    }
}