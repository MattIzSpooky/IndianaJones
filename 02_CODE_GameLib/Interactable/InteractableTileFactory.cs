using System;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Trap;

namespace CODE_GameLib.Interactable
{
    public class InteractableTileFactory
    {
        private const string Key = "key";
        private const string SankaraStone = "sankara stone";
        private const string BoobyTrap = "boobietrap";
        private const string PressurePlate = "pressure plate";
        private const string DisappearingBoobyTrap = "disappearing boobietrap";
        
        // Not part of JSON.
        private const string Hallway = "hallway";

        public InteractableTile Create(string type, Room room, int x, int y, object arg)
        {
            return type switch
            {
                Key => new Key(room, x, y, (string)arg),
                SankaraStone => new SankaraStone(room, x, y),
                BoobyTrap => new BoobyTrap(room, x, y, int.Parse((string)arg)),
                PressurePlate => new PressurePlate(room, x, y),
                DisappearingBoobyTrap => new DisappearingBoobyTrap(room, x, y, int.Parse((string)arg)),
                Hallway => new InteractableHallway(room, x, y, (Hallway)arg),
                _ => throw new ArgumentException($"Type: {type} is not valid")
            };
        }
    }
}