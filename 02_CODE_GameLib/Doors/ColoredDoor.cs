﻿using System.Collections.Immutable;
using System.Drawing;
using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ColoredDoor : IDoor
    {
        public bool IsOpen { get; private set;  }
        public bool TriedToOpen { get; private set; }
        public Color Color { get; }

        public ColoredDoor(string color)
        {
            Color = Color.FromName(color);
        }

  
        public bool Open(ImmutableDictionary<Cheat, bool> cheats, Player player)
        {
            TriedToOpen = true;
            
            if (cheats[Cheat.MoveThroughDoors]) return true;
            
            if (player.HasKey(Color))
            {
                IsOpen = true;
            }

            return IsOpen;
        }
    }
}