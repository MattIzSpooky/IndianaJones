﻿namespace CODE_GameLib.Interactable.Trap
{
    public class HurtingTrap : IInteractable
    {
        public int X { get; }
        public int Y { get; }
        
        public void InteractWith(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}