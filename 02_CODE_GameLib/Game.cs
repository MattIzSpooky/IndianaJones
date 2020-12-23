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

            CurrentRoom = rooms.First(e => e.Interactables.Any(i => i is Player));
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
                RunCollisions();
                CheckGameEnd();
            }

            Notify(this);
        }

        public void PlayerAttack()
        {
            Player.Attack(CurrentRoom.Enemies);
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

        private void RunCollisions()
        {
            foreach (var interactable in CurrentRoom.Interactables)
            {
                RunSingleCollision(interactable);
            }
        }

        public void RunSingleCollision(IInteractable interactable)
        {
            foreach (var other in CurrentRoom.Interactables)
            {
                if (other.Equals(interactable)) continue; // Skip iteration if they are the exact same object

                if (other.AllowedToCollideWith(Cheats, interactable) && other.CollidesWith(interactable))
                    other.InteractWith(this, interactable);
            }
        }
    }
}