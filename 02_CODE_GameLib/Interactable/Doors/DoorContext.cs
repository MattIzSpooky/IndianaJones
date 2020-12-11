namespace CODE_GameLib.Interactable.Doors
{
    public class DoorContext
    {
        public IDoor _door;

        public DoorContext(IDoor door)
        {
            _door = door;
        }

        public bool Open(Player player) => _door.Open(player);
    }
}