namespace CODE_GameLib.Interactable.Trap
{
    public class DisappearingBoobyTrap : BoobyTrap
    {
        public DisappearingBoobyTrap(Room room, int x, int y, int damage) : base(room, x, y, damage)
        {
        }

        public override void InteractWith(Player player)
        {
            base.InteractWith(player);
            
            Room.Remove(this);
        }

        public override string GetType() => "DisappearingBoobyTrap";
    }
}