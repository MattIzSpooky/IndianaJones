﻿using System;
using System.Collections.Generic;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Player : IPosition
    {
        public int Lives { get; }

        public int X { get; private set; }
        public int Y { get; private set; }

        private List<IInteractable> _interactables = new List<IInteractable>();

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
        
        public void Move(WindRose direction)
        {
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

        public void GetHurt(int damage)
        {
        }
    }
}