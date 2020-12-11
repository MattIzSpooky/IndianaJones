using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ClosingGate : IDoor
    {
        public Tile Tile { get; }

        private bool _entered;

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
            if (!_entered) _entered = true;

            return !_entered;
        }
    }
}