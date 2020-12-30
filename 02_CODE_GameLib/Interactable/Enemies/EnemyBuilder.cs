using System;
using CODE_TempleOfDoom_DownloadableContent;
using Utils;

namespace CODE_GameLib.Interactable.Enemies
{
    public class EnemyBuilder : IBuilder<Enemy>
    {
        public string Type { private get; set; }
        public int X { private get; set; }
        public int Y { private get; set; }
        public int MinX { private get; set; }
        public int MinY { private get; set; }
        public int MaxX { private get; set; }
        public int MaxY { private get; set; }

        private const string Horizontal = "horizontal";
        private const string Vertical = "vertical";
        private const int NumberOfLives = 1;

        public Enemy GetResult()
        {
            return Type switch
            {
                Horizontal => new HorizontallyMovingEnemy(NumberOfLives, X, Y, MinX, MaxX),
                Vertical => new VerticallyMovingEnemy(NumberOfLives, X, Y, MinY, MaxY),
                _ => throw new ArgumentException($"Type: {Type} is not valid")
            };
        }

        public void Reset()
        {
            Type = null;

            X = 0;
            Y = 0;

            MinX = 0;
            MinY = 0;

            MaxX = 0;
            MaxY = 0;
        }
    }
}