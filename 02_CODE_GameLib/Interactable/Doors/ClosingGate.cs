using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ClosingGate : IDoor
    {
        public Tile Tile { get; }
        
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
            throw new System.NotImplementedException();
        }
    }
}