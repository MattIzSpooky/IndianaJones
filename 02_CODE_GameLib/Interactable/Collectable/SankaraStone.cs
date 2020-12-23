namespace CODE_GameLib.Interactable.Collectable
{
    public class SankaraStone : InteractableTile
    {
        public SankaraStone(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(Game context, IInteractable other)
        {
            if (!(other is Player player)) return;

            player.AddToInventory(this);
            player.Score++;

            Room.Remove(this);
        }
    }
}