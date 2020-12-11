namespace CODE_GameLib.Interactable.Doors
{
    public class DoorContext
    {
        public IDoor Door { get; }

        public DoorContext(IDoor door)
        {
            Door = door;
        }

        public bool Open(Player player) => Door.Open(player);
    }
}