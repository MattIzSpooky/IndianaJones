using System;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Trap;

namespace CODE_GameLib.Interactable
{
    public class InteractableFactory
    {
        private const string Key = "key";
        private const string SankaraStone = "sankara stone";
        private const string BoobyTrap = "boobietrap";
        private const string PressurePlate = "pressure plate";
        private const string DisappearingBoobyTrap = "disappearing boobietrap";

        public IInteractable Create(string type, Room room, int x, int y, string args)
        {
            return type switch
            {
                Key => new Key(room, x, y, args),
                SankaraStone => new SankaraStone(room, x, y),
                BoobyTrap => new BoobyTrap(room, x, y, int.Parse(args)),
                PressurePlate => new PressurePlate(room, x, y),
                DisappearingBoobyTrap => new DisappearingBoobyTrap(room, x, y, int.Parse(args)),
                _ => throw new ArgumentException($"Type: {type} is not valid")
            };
        }
    }
}