using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ClosingGate : IDoor
    {
        public Tile Tile { get; }

        private bool _isOpen = true;

        public ClosingGate()
        {
            Tile = new Tile
            {
                Character = '∩',
                Color = Color.White
            };
        }

        public bool Open(Player player)
        {
            if (!_isOpen) return _isOpen;
            
            _isOpen = false;
            
            return true;
        }
    }
}