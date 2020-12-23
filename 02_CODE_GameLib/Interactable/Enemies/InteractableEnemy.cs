using System;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Interactable.Enemies
{
    public class InteractableEnemy : InteractableTile, IMovable, ILiving, IObserver<Enemy>
    {
        public int NumberOfLives => _enemy.NumberOfLives;

        private readonly Enemy _enemy;

        private const int Damage = 1;

        public InteractableEnemy(Room room, int x, int y, Enemy enemy) : base(room, x, y)
        {
            _enemy = enemy;

            _enemy.Subscribe(this);
        }

        public override void InteractWith(Game context, IInteractable other)
        {
            if (other is Player player && !context.Cheats[Cheat.Invincible])
            {
                player.GetHurt(Damage);
            }
        }

        public void Move() => _enemy.Move();

        public void GetHurt(int damage)
        {
            _enemy.GetHurt(damage);

            if (NumberOfLives <= 0) Room.Remove(this);
        }

        public void OnCompleted() => throw new NotImplementedException();

        public void OnError(Exception error) => throw new NotImplementedException();

        public void OnNext(Enemy value)
        {
            X = value.CurrentXLocation;
            Y = value.CurrentYLocation;
        }
    }
}