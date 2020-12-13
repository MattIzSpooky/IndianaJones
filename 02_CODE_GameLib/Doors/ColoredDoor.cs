using System.Drawing;
using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ColoredDoor : IDoor
    {
        public Color Color { get; }

        public ColoredDoor(string color)
        {
            Color = Color.FromName(color);
        }

        public bool Open(Player player) => player.HasKey(Color);
    }
}