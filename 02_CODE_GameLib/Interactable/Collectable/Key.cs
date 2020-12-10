using System.Drawing;

namespace CODE_GameLib.Interactable.Collectable
{
    public class Key : InteractableTile
    {
        public Key(Room room, int x, int y, string color) : base(room, x, y, new Tile
        {
            Color = Color.FromName(color),
            Character = 'K'
        })
        {
        }

        public override bool CanInteractWith(IInteractable other)
        {
            return other is Player player && player.X == X && player.Y == Y;
        }

        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) return;
            
            player.AddToInventory(this);

            Room.Remove(this);
        }
    }
}