using System.Drawing;
using CODE_GameLib;

namespace CODE_Frontend.ViewModels
{
    public struct ViewableHallway
    {
        public ViewableWindRose Direction { get; set; }
        public char Character { get; set; }
        public Color Color { get; set; }
    }
}