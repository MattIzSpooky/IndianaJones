using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Player : IPosition, IInteractable
    {
        public int Lives { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        private List<IInteractable> _interactables = new List<IInteractable>();

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

        public void EnterRoom(int roomWidth, int roomHeight, WindRose direction)
        {
            switch (direction)
            {
                case WindRose.North:
                    Y = roomHeight;
                    X = roomWidth / 2;
                    break;
                case WindRose.East:
                    Y = roomHeight / 2;
                    X = 0;
                    break;
                case WindRose.South:
                    Y = 0;
                    X = roomWidth / 2;
                    break;
                case WindRose.West:
                    Y = roomHeight / 2;
                    X = roomWidth;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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