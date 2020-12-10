using System.Drawing;
using System.Numerics;

namespace CODE_Frontend.Views
{
    public struct ViewableItem
    {
        public Vector2 Position { get; set; }
        public string Type { get; set; }
        public Color Color { get; set; }
    }
}