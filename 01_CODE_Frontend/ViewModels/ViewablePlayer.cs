using System.Numerics;

namespace CODE_Frontend.ViewModels
{
    public struct ViewablePlayer
    {
        public Vector2 Position { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
    }
}