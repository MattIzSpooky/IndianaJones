using System.Numerics;

namespace CODE_Frontend.ViewModels
{
    public struct PlayerViewModel
    {
        public Vector2 Position { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
    }
}