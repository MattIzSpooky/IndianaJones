namespace CODE_GameLib.Interactable.Trap
{
    public class BoobyTrap : InteractableTile
    {
        private readonly int _damage;

        public BoobyTrap(Room room, int x, int y, int damage) : base(room, x, y)
        {
            _damage = damage;
        }

        public override void InteractWith(Game context, IInteractable other)
        {
            if (other is Player player && !context.Cheats[Cheat.Invincible])
            {
                player.GetHurt(_damage);
            }
        }
    }
}