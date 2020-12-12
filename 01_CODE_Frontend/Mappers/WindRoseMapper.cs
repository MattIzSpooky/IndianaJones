using System;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class WindRoseMapper : IMapper<WindRose, ViewableWindRose>
    {
        public ViewableWindRose MapTo(WindRose @from)
        {
            return from switch
            {
                WindRose.North => ViewableWindRose.North,
                WindRose.East => ViewableWindRose.East,
                WindRose.South => ViewableWindRose.South,
                WindRose.West => ViewableWindRose.West,
                _ => throw new ArgumentException($"{from} is not a valid argument")
            };
        }
    }
}