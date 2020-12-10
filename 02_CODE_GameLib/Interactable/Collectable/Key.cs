using System.Drawing;
using System.Numerics;

namespace CODE_GameLib.Interactable.Collectable
{
    public class Key : InteractableTile
    {
        public Color Color { get; }

        public Key(Room room, int x, int y, string color) : base(room, x, y)
        {
            Color = Color.FromName(color);
        }

        public override void InteractWith(Player player)
        {
            player.AddToInventory(this);
            
            Room.Remove(this);
        }

        public override string GetType() => "Key";
    }
}