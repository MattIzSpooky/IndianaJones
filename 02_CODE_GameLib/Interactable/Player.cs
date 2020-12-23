using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        public Direction LastDirection { get; private set; }

        private readonly List<IInteractable> _inventory = new List<IInteractable>();

        public Player(int lives, int startX, int startY)
        {
            Lives = lives;
            X = startX;
            Y = startY;
        }

        public void AddToInventory(IInteractable interactable) => _inventory.Add(interactable);

        public void EnterRoom(Room room, int x, int y)
        {
            X = x;
            Y = y;

            room.Player = this;
        }
        
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
        /// Sets the current position to the next position, then checks if the move is allowed.
        /// It resets the position if the player cannot move to that new position. Returns the result of the attempted move.
        /// </summary>
        /// <param name="room">The room the player resides in.</param>
        /// <param name="direction">The direction the player is moving in.</param>
        /// <param name="cheats">The current cheats</param>
        /// <returns>Whether the move was successful or not</returns>
        public bool AttemptMove(Room room, Direction direction, ImmutableDictionary<Cheat, bool> cheats)
        {
            var previousX = X;
            var previousY = Y;
            var previousDirection = LastDirection;

            var (x, y) = CalculateNextPosition(direction);

            X = x;
            Y = y;
            LastDirection = direction;

            // Reset position if out of bounds.
            // Direction is not reset because you will still be interacting with an InteractableHallway and thus be teleported.
            if (IsOutOfBounds(room))
            {
                X = previousX;
                Y = previousY;
                
                return false;
            }

            if (!room.Interactables.Any(r => !r.AllowedToCollideWith(cheats, this) && r.CollidesWith(this))) 
                return true;

            X = previousX;
            Y = previousY;
            LastDirection = previousDirection;

            return false;
        }

        private bool IsOutOfBounds(Room room) => X >= room.Width + 1 ||
                                                 Y >= room.Height + 1 ||
                                                 X == -1 ||
                                                 Y == -1;

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
        public bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other) => throw new NotImplementedException();
        public void InteractWith(Game gameContext, IInteractable player) => throw new NotImplementedException();
    }
}