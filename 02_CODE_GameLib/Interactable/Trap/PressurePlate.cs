using System.Drawing;

namespace CODE_GameLib.Interactable.Trap
{
    public class PressurePlate : InteractableTile
    {
        public PressurePlate(Room room, int x, int y) : base(room, x, y, new Tile
        {
            Color = Color.White,
            Character = 'T'
        })
        {
        }

        public override void InteractWith(IInteractable other)
        {
            throw new System.NotImplementedException();
        }
    }
}