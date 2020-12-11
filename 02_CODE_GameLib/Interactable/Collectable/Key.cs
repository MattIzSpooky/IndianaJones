using System.Drawing;

namespace CODE_GameLib.Interactable.Collectable
{
    public class Key : InteractableTile
    {
        public Color Color { get; }
        public Key(Room room, int x, int y, string color) : base(room, x, y)
        {
            Color = Color.FromName(color);
        }

        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) return;
            
            player.AddToInventory(this);

            Room.Remove(this);
        }
    }
}