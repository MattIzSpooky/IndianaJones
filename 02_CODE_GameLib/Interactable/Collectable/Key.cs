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

        public override void InteractWith(Player player)
        {
            player.AddToInventory(this);

            Room.Remove(this);
        }
    }
}