using System.Drawing;
using CODE_Frontend.Views;
using CODE_GameLib;
using CODE_GameLib.Interactable.Doors;

namespace CODE_Frontend.Mappers
{
    public class HallwayMapper : IMapper<Hallway, ViewableHallway>
    {
        public int RoomId { private get; set; }
        public ViewableHallway MapTo(Hallway from)
        {
            var direction = from.GetDirectionByRoom(RoomId);

            if (from.DoorContext != null)
            {
                return from.DoorContext.Door switch
                {
                    ClosingGate _ => new ViewableHallway {Character = '∩', Color = Color.White, Direction = direction},
                    ColoredDoor coloredDoor => new ViewableHallway
                        {Character = '=', Color = coloredDoor.Color, Direction = direction},
                    ToggleDoor _ => new ViewableHallway {Character = '┴', Color = Color.White, Direction = direction},
                };
            }

            return new ViewableHallway {Character = ' ', Color = Color.Empty, Direction = direction};
        }
    }
}