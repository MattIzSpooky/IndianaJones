using System.Drawing;

namespace CODE_GameLib.Interactable.Doors
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