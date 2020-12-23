using System;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Interactable.Enemies
{
    public class EnemyBuilder
    {
        private string _type;
        private int _x;
        private int _y;
        private int _minX;
        private int _minY;
        private int _maxX;
        private int _maxY;
        
        private const string Horizontal = "horizontal";
        private const string Vertical = "vertical";
        private const int NumberOfLives = 2;
        public void SetType(string type) => _type = type;
        public void SetX(int x) => _x = x;
        public void SetY(int y) => _y = y;
        public void SetMinX(int minX) => _minX = minX;
        public void SetMinY(int minY) => _minY = minY;
        public void SetMaxX(int maxX) => _maxX = maxX;
        public void SetMaxY(int maxY) => _maxY = maxY;
        
        public Enemy GetResult()
        {
            return _type switch
            {
                Horizontal => new HorizontallyMovingEnemy(NumberOfLives, _x, _y,_minX, _maxX),
                Vertical => new VerticallyMovingEnemy(NumberOfLives, _x, _y,_minY, _maxY),
                _ => throw new ArgumentException($"Type: {_type} is not valid")
            };
        }

        public void Reset()
        {
            SetType(null);
            
            SetX(0);
            SetY(0);
            
            SetMinX(0);
            SetMinY(0);
            
            SetMaxX(0);
            SetMaxY(0);
        }
    }
}