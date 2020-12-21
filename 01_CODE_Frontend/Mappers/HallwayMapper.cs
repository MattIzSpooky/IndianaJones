using System;
using System.Drawing;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using CODE_GameLib.Doors;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class HallwayMapper : IMapper<Hallway, HallwayViewModel>
    {
        public int RoomId { private get; set; }

        private const char ColoredDoor = '=';
        private const char ClosingGate = '∩';
        private const char ToggleDoor = '┴';
        private const char EmptyHallway = ' ';

        private readonly IMapper<Direction, ViewableDirection> _directionMapper = new DirectionMapper();

        public HallwayViewModel MapTo(Hallway from)
        {
            var direction = _directionMapper.MapTo(from.GetDirectionByRoom(RoomId));

            if (from.DoorContext != null)
            {
                return from.DoorContext.Door switch
                {
                    ClosingGate _ => new HallwayViewModel
                        {Character = ClosingGate, Color = Color.White, Direction = direction},
                    ColoredDoor coloredDoor => new HallwayViewModel
                        {Character = ColoredDoor, Color = coloredDoor.Color, Direction = direction},
                    ToggleDoor _ => new HallwayViewModel
                        {Character = ToggleDoor, Color = Color.White, Direction = direction},
                    _ => throw new ArgumentException("Unknown door type")
                };
            }

            return new HallwayViewModel {Character = EmptyHallway, Color = Color.Empty, Direction = direction};
        }
    }
}