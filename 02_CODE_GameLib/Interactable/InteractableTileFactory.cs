﻿using System;
using CODE_GameLib.Connections;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Connections;
using CODE_GameLib.Interactable.Enemies;
using CODE_GameLib.Interactable.Special;
using CODE_GameLib.Interactable.Trap;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Interactable
{
    public class InteractableTileFactory
    {
        private const string Key = "key";
        private const string SankaraStone = "sankara stone";
        private const string BoobyTrap = "boobietrap";
        private const string PressurePlate = "pressure plate";
        private const string DisappearingBoobyTrap = "disappearing boobietrap";
        private const string InteractableLadder = "ladder";
        private const string Ice = "ice";

        // Not part of JSON.
        private const string Hallway = "hallway";
        private const string Enemy = "enemy";

        public InteractableTile Create(string type, Room room, int x, int y, object arg)
        {
            return type switch
            {
                Key => new Key(room, x, y, (string) arg),
                Ice => new IceTile(room, x, y),
                SankaraStone => new SankaraStone(room, x, y),
                BoobyTrap => new BoobyTrap(room, x, y, int.Parse((string) arg)),
                PressurePlate => new PressurePlate(room, x, y),
                DisappearingBoobyTrap => new DisappearingBoobyTrap(room, x, y, int.Parse((string) arg)),
                Hallway => new InteractableHallway(room, x, y, (Hallway) arg),
                InteractableLadder => new Ladder(room, x, y, (Room) arg),
                Enemy => new InteractableEnemy(room, x, y, (Enemy) arg),
                _ => throw new ArgumentException($"Type: {type} is not valid")
            };
        }
    }
}