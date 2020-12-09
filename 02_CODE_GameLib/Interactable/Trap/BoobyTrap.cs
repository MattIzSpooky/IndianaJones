﻿namespace CODE_GameLib.Interactable.Trap
{
    public class BoobyTrap : InteractableTile
    {
        private readonly int _damage;

        public BoobyTrap(Room room, int x, int y, int damage) : base(room, x, y)
        {
            _damage = damage;
        }

        public override void InteractWith(Player player)
        {
            player.GetHurt(_damage);
        }

        public override string GetType() => "BoobyTrap";
    }
}