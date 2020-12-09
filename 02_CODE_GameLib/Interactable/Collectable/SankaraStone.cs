namespace CODE_GameLib.Interactable.Collectable
{
    public class SankaraStone : InteractableTile
    {
        public SankaraStone(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(Player player)
        {
            player.AddToInventory(this);
            
            Room.Remove(this);
        }

        public override string GetType() => "SankaraStone";
    }
}