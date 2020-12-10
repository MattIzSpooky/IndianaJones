using System;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Wall: InteractableTile
    {
        public Wall(Room room, int x, int y, Tile tile) : base(room, x, y, tile)
        {
        }

        public override bool CanInteractWith(IInteractable other)
        {
            return other is Player player;
        }

        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) 
                return;

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("Masallah");
            }
        }
    }
}