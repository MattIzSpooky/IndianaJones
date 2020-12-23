using System;
using System.Drawing;
using System.Numerics;
using CODE_Frontend.ViewModels;
using CODE_GameLib.Doors;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Connections;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class HallwayMapper : IMapper<InteractableHallway, InteractableViewModel>
    {
        private const char ColoredDoor = '=';
        private const char ClosingGate = '∩';
        private const char ToggleDoor = '┴';
        private const char EmptyHallway = ' ';

        public InteractableViewModel MapTo(InteractableHallway from)
        {
            var hallway = from.Hallway;

            if (hallway.Door == null)
            {
                return new InteractableViewModel
                {
                    Character = EmptyHallway,
                    Color = Color.Empty,
                    Position = new Vector2(from.X, from.Y)
                };
            }

            var door = hallway.Door switch
            {
                ClosingGate _ => new InteractableViewModel()
                    {Character = ClosingGate, Color = Color.White},
                ColoredDoor coloredDoor => new InteractableViewModel
                    {Character = ColoredDoor, Color = coloredDoor.Color},
                ToggleDoor _ => new InteractableViewModel
                    {Character = ToggleDoor, Color = Color.White},
                _ => throw new ArgumentException("Unknown door type")
            };

            door.Position = new Vector2(from.X, from.Y);

            return door;
        }
    }
}