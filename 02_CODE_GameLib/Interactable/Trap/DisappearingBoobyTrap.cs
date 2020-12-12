namespace CODE_GameLib.Interactable.Trap
{
    public class DisappearingBoobyTrap : BoobyTrap
    {
        public DisappearingBoobyTrap(Room room, int x, int y, int damage) : base(room, x, y, damage)
        {
        }

        public override void InteractWith(IInteractable other)
        {
            if (!(other is Player player)) return;

            base.InteractWith(player);
            Room.Remove(this);
        }
    }
}