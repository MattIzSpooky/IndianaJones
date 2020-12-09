using System;

namespace CODE_GameLib
{
    public enum WindRose
    {
        North,
        East,
        South,
        West
    }

    public static class WindRoseExtensions
    {
        public static WindRose Flip(this WindRose windRose)
        {
            return windRose switch
            {
                WindRose.North => WindRose.South,
                WindRose.South => WindRose.North,
                WindRose.East => WindRose.West,
                WindRose.West => WindRose.East,
                _ => throw new ArgumentException($"{windRose} is not a valid argument")
            };
        }
    }
}