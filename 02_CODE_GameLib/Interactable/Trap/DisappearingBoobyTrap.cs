namespace CODE_GameLib.Interactable.Trap
{
    public class DisappearingBoobyTrap : BoobyTrap
    {
        public DisappearingBoobyTrap(Room room, int x, int y, int damage) : base(room, x, y, damage)
        {
        }

        public override void InteractWith(Game context, IInteractable other)
        {
            if (!(other is Player player)) return;

            base.InteractWith(context, player);
            Room.Remove(this);
        }
    }
}