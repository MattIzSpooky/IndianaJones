using System.Drawing;
using CODE_GameLib;

namespace CODE_Frontend.ViewModels
{
    public struct ViewableHallway
    {
        // TODO: Should not reference Windrose.
        public WindRose Direction { get; set; }
        public char Character { get; set; }
        public Color Color { get; set; }
    }
}