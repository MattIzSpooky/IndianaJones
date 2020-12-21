using System;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class DirectionMapper : IMapper<Direction, ViewableDirection>
    {
        public ViewableDirection MapTo(Direction from)
        {
            return from switch
            {
                Direction.North => ViewableDirection.North,
                Direction.East => ViewableDirection.East,
                Direction.South => ViewableDirection.South,
                Direction.West => ViewableDirection.West,
                _ => throw new ArgumentException($"{from} is not a valid argument")
            };
        }
    }
}