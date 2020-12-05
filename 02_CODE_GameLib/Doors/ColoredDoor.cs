using System.Drawing;

namespace CODE_GameLib.Doors
{
    public class ColoredDoor : IDoor
    {
        private Color _color;

        public ColoredDoor(string color)
        {
            _color = Color.FromName(color);
        }

        public bool Open(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}