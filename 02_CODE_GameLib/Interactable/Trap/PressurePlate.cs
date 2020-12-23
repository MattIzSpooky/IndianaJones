using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib.Interactable.Trap
{
    public class PressurePlate : InteractableTile
    {
        public PressurePlate(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(Game context, IInteractable other)
        {
            var toggleDoors = Room.Doors
                .Where(d => d is ToggleDoor)
                .Cast<ToggleDoor>();

            foreach (var toggleDoor in toggleDoors)
            {
                toggleDoor.Toggle();
            }
        }
    }
}