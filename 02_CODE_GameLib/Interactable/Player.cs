using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Interactable.Collectable;

namespace CODE_GameLib.Interactable
{
    public class Player : IInteractable
    {
        public int Lives { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Score { get; set; }

        private readonly List<IInteractable> _inventory = new List<IInteractable>();

        public Player(int lives, int startX, int startY)
        {
            Lives = lives;
            X = startX;
            Y = startY;
        }

        public void AddToInventory(IInteractable interactable) => _inventory.Add(interactable);

        public void EnterRoom(Room room, Direction direction)
        {
            var (x, y) = CalculatePositionInRoom(room, direction);

            X = x;
            Y = y;

            room.Player = this;
        }

        private (int x, int y) CalculatePositionInRoom(Room room, Direction direction)
        {
            int x;
            int y;

            switch (direction)
            {
                case Direction.North:
                    y = room.Height;
                    x = room.Width / 2;
                    break;
                case Direction.East:
                    y = room.Height / 2;
                    x = 0;
                    break;
                case Direction.South:
                    y = 0;
                    x = room.Width / 2;
                    break;
                case Direction.West:
                    y = room.Height / 2;
                    x = room.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (x, y);
        }

        /// <summary>
        /// Tries to set the position on the next direction.
        /// Resets if it cannot move.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="direction"></param>
        public void TryMove(Room room, Direction direction)
        {
            var previousX = X;
            var previousY = Y;
            
            var (x, y) = CalculateNextPosition(direction);
            
            X = x;
            Y = y;

            if (!room.Interactables.Any(r => r is Wall && r.CollidesWith(this))) return;
            
            X = previousX;
            Y = previousY;
        }

        private (int x, int y) CalculateNextPosition(Direction direction)
        {
            var x = X;
            var y = Y;

            switch (direction)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return (x, y);
        }

        public bool HasKey(Color color) =>
            _inventory.Any(e => e is Key key && key.Color == color);

        public void GetHurt(int damage) => Lives -= damage;

        public bool CollidesWith(IInteractable other) => true;

        public void InteractWith(IInteractable player) => throw new NotImplementedException();
    }
}