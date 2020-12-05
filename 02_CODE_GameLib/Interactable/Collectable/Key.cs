using System.Drawing;

namespace CODE_GameLib.Interactable.Collectable
{
    public class Key : IInteractable
    {
        public int X { get; }
        public int Y { get; }

        public Color Color { get; set; }

        public void InteractWith(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}