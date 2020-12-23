using System.Collections.Generic;
using System.Collections.Immutable;
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

        public ImmutableDictionary<Cheat, bool> Cheats => _cheats.ToImmutableDictionary();

        private readonly Dictionary<Cheat, bool> _cheats = new Dictionary<Cheat, bool>()
        {
            {Cheat.Invincible, false},
            {Cheat.MoveThroughDoors, false}
        };

        public Game(IEnumerable<Room> rooms, Player player, int stones)
        {
            _rooms = rooms;
            Player = player;
            Stones = stones;

            CurrentRoom = _rooms.First(e => e.Player != null);
        }

        public void MovePlayer(Direction direction)
        {
            if (Player.AttemptMove(CurrentRoom, direction))
            {
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

        private void CheckGameEnd() => HasEnded = Player.Score == Stones || Player.Lives <= 0;

        private void CheckCollides()
        {
            foreach (var interactableTile in CurrentRoom.Interactables)
            {
                if (interactableTile.AllowedToCollideWith(Player) && interactableTile.CollidesWith(Player))
                    interactableTile.InteractWith(this, Player);
            }
        }
    }
}