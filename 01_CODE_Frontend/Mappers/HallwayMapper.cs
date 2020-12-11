using System.Drawing;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using CODE_GameLib.Interactable.Doors;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class HallwayMapper : IMapper<Hallway, ViewableHallway>
    {
        public int RoomId { private get; set; }
        
        private const char ColoredDoor = '=';
        private const char ClosingGate = '∩';
        private const char ToggleDoor = '┴';
        private const char EmptyHallway = ' ';

        public ViewableHallway MapTo(Hallway from)
        {
            var direction = from.GetDirectionByRoom(RoomId);

            if (from.DoorContext != null)
            {
                return from.DoorContext.Door switch
                {
                    ClosingGate _ => new ViewableHallway
                        {Character = ClosingGate, Color = Color.White, Direction = direction},
                    ColoredDoor coloredDoor => new ViewableHallway
                        {Character = ColoredDoor, Color = coloredDoor.Color, Direction = direction},
                    ToggleDoor _ => new ViewableHallway
                        {Character = ToggleDoor, Color = Color.White, Direction = direction},
                };
            }

            return new ViewableHallway {Character = EmptyHallway, Color = Color.Empty, Direction = direction};
        }
    }
}