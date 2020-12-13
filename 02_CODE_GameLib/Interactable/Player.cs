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
        public bool CanMove { get; set; }

        private readonly List<IInteractable> _inventory = new List<IInteractable>();

        public Player(int lives, int startX, int startY)
        {
            Lives = lives;
            X = startX;
            Y = startY;
        }

        public void AddToInventory(IInteractable interactable) => _inventory.Add(interactable);

        public void EnterRoom(Room room, WindRose direction)
        {
            var (x, y) = CalculatePositionInRoom(room, direction);

            X = x;
            Y = y;

            room.Player = this;
        }

        private (int x, int y) CalculatePositionInRoom(Room room, WindRose direction)
        {
            int x;
            int y;

            switch (direction)
            {
                case WindRose.North:
                    y = room.Height;
                    x = room.Width / 2;
                    break;
                case WindRose.East:
                    y = room.Height / 2;
                    x = 0;
                    break;
                case WindRose.South:
                    y = 0;
                    x = room.Width / 2;
                    break;
                case WindRose.West:
                    y = room.Height / 2;
                    x = room.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (x, y);
        }

        public void TryMove(WindRose direction)
        {
            CanMove = true;

            switch (direction)
            {
                case WindRose.North:
                    Y--;
                    break;
                case WindRose.East:
                    X++;
                    break;
                case WindRose.South:
                    Y++;
                    break;
                case WindRose.West:
                    X--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void RevertMove(WindRose direction)
        {
            switch (direction)
            {
                case WindRose.North:
                    Y++;
                    break;
                case WindRose.East:
                    X--;
                    break;
                case WindRose.South:
                    Y--;
                    break;
                case WindRose.West:
                    X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public bool HasKey(Color color) =>
            _inventory.Any(e => e is Key key && key.Color == color);

        public void GetHurt(int damage) => Lives -= damage;

        public bool CanInteractWith(IInteractable other) => true;

        public void InteractWith(IInteractable player) => throw new NotImplementedException();
    }
}