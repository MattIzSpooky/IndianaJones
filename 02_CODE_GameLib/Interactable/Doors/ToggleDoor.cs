using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ToggleDoor : IDoor
    {
        public Tile Tile { get; }
        public bool IsOpen;

        public ToggleDoor()
        {
            Tile = new Tile
            {
                Character = '┴',
                Color = Color.White
            };
        }

        public bool Open(Player player) => IsOpen;

        public void Toggle() => IsOpen = !IsOpen;
    }
}