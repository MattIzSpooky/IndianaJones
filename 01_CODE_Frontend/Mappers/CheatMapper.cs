using System;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class CheatMapper : IMapper<Cheat, ViewableCheat>
    {
        public ViewableCheat MapTo(Cheat from)
        {
            return from switch
            {
                Cheat.Invincible => ViewableCheat.Invincible,
                Cheat.MoveThroughDoors => ViewableCheat.MoveThroughDoors,
                _ => throw new ArgumentException($"{from} is not a valid argument")
            };
        }
    }
}