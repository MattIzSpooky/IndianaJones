namespace CODE_GameLib.Interactable.Trap
{
    public class BoobyTrap : InteractableTile
    {
        private readonly int _damage;

        public BoobyTrap(Room room, int x, int y, int damage) : base(room, x, y)
        {
            _damage = damage;
        }

        public override void InteractWith(Game gameContext, IInteractable other)
        {
            var invincibilityCheatEnabled = gameContext.Cheats[Cheat.Invincible];
            
            if (other is Player player && !invincibilityCheatEnabled)
            {
                player.GetHurt(_damage);
            }
        }
    }
}