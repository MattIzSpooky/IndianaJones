﻿using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Player : IPosition
    {
        public int Lives { get; }
        
        public int X { get; }
        public int Y { get; }

        private IInteractable _interactables;

        public void Move()
        {
        }

        public void GetHurt(int damage)
        {
        }
    }
}