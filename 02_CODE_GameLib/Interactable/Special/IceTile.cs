using System.Collections.Immutable;
using CODE_GameLib.Interactable.Enemies;

namespace CODE_GameLib.Interactable.Special
{
    public class IceTile : InteractableTile
    {
        public IceTile(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other) =>
            other is Player || other is InteractableEnemy;

        public override void InteractWith(Game context, IInteractable other)
        {
            switch (other)
            {
                case Player player:
                    if (player.AttemptMove(Room, player.LastDirection, context.Cheats))
                    {
                        context.RunSingleCollision(player);
                    }
                    break;
                case InteractableEnemy enemy:
                    enemy.Move();
                    context.RunSingleCollision(enemy);
                    break;
            }
        }
    }
}