using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Collectable;

namespace CODE_GameLib
{
    public class Player : IPosition, IInteractable
    {
        public int Lives { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly List<IInteractable> _interactables = new List<IInteractable>();

        public int Score { get; set; }
        public bool CanMove { get; set; }

        public Player(int lives, int startX, int startY)
        {
            Lives = lives;
            X = startX;
            Y = startY;
        }

        public void AddToInventory(IInteractable interactable)
        {
            _interactables.Add(interactable);
        }

        public void EnterRoom(Room room, WindRose direction)
        {
            switch (direction)
            {
                case WindRose.North:
                    Y = room.Height;
                    X = room.Width / 2;
                    break;
                case WindRose.East:
                    Y = room.Height / 2;
                    X = 0;
                    break;
                case WindRose.South:
                    Y = 0;
                    X = room.Width / 2;
                    break;
                case WindRose.West:
                    Y = room.Height / 2;
                    X = room.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            room.Player = this;
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

        public bool HasKey(Color color) => _interactables.Any(e => e is Key key && key.Tile.Color == color);

        public void GetHurt(int damage)
        {
            Lives -= damage;
        }

        public bool CanInteractWith(IInteractable other)
        {
            return true;
        }

        public void InteractWith(IInteractable player)
        {
            throw new NotImplementedException();
        }
    }
}