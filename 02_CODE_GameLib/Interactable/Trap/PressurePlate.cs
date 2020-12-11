using System.Drawing;
using System.Linq;
using CODE_GameLib.Interactable.Doors;

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
            var toggleDoors = Room.GetDoors()
                .Select(e => e.Door)
                .Where(d => d is ToggleDoor)
                .Cast<ToggleDoor>();

            foreach (var toggleDoor in toggleDoors)
            {
                toggleDoor.Toggle();
            }
        }
    }
}