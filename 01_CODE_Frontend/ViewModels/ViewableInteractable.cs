using System.Drawing;
using System.Numerics;

namespace CODE_Frontend.ViewModels
{
    public struct ViewableInteractable
    {
        public Vector2 Position { get; set; }
        public char Character { get; set; }
        public Color Color { get; set; }
    }
}