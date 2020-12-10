using System.Drawing;

namespace CODE_GameLib.Interactable.Collectable
{
    public class SankaraStone : InteractableTile
    {
        public SankaraStone(Room room, int x, int y) : base(room, x, y, new Tile
        {
            Color = Color.Orange,
            Character = 'S'
        })
        {
        }

        // TODO: Make a collectable class so that they can share this functionality.
        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) return;
            
            player.AddToInventory(this);
            player.Score++;
            
            Room.Remove(this);
        }
    }
}