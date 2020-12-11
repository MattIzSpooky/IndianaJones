﻿using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Wall: InteractableTile
    {
        public Wall(Room room, int x, int y) : base(room, x, y)
        {
        }
        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) 
                return;

            player.CanMove = false;
        }
    }
}