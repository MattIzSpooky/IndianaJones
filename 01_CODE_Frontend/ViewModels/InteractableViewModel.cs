using System.Drawing;

namespace CODE_Frontend.ViewModels
{
    public struct InteractableViewModel
    {
        public ViewablePosition Position { get; set; }
        public char Character { get; set; }
        public Color Color { get; set; }
    }
}