﻿using System;
using System.Drawing;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Wall: InteractableTile
    {
        public Wall(Room room, int x, int y) : base(room, x, y, new Tile
        {
            Character = '#',
            Color = Color.Yellow
        })
        {
        }

        public override bool CanInteractWith(IInteractable other)
        {
            // return other is Player player && player.X == X && player.Y == Y;
            return true;
        }
        

        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) 
                return;

            player.CanMove = false;
        }
    }
}