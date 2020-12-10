namespace CODE_GameLib.Interactable.Collectable
{
    public class SankaraStone : InteractableTile
    {
        public SankaraStone(Room room, int x, int y) : base(room, x, y)
        {
        }

        // TODO: Make a collectable class so that they can share this functionality.
        public override void InteractWith(Player player)
        {
            player.AddToInventory(this);
            
            Room.Remove(this);
        }

        public override string GetType() => "SankaraStone";
    }
}