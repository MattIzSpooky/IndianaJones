using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
{
    public class ColoredDoor : IDoor
    {
        public Tile Tile { get; }

        public ColoredDoor(string color)
        {
            Tile = new Tile
            {
                Character = '=',
                Color = Color.FromName(color)
            };
        }

        public bool Open(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}