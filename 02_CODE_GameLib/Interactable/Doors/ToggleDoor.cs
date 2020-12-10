using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ToggleDoor : IDoor
    {
        public Tile Tile { get; }

        public ToggleDoor()
        {
            Tile = new Tile
            {
                Character = '┴',
                Color = Color.White
            };
        }
        public bool Open(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}