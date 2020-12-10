﻿using System.Drawing;

namespace CODE_GameLib.Interactable.Trap
{
    public class BoobyTrap : InteractableTile
    {
        private readonly int _damage;

        public BoobyTrap(Room room, int x, int y, int damage) : base(room, x, y,  new Tile
        {
            Color = Color.White,
            Character = 'O'
        })
        {
            _damage = damage;
        }


        public override bool CanInteractWith(IInteractable other)
        {
            return true;
        }

        public override void InteractWith(IInteractable other)
        {
            if (other is Player player)
            {
                player.GetHurt(_damage);
            }
        }
    }
}