namespace CODE_GameLib.Interactable.Doors
{
    public class DoorContext : IInteractable
    {
        public IDoor Door { get; }

        public DoorContext(IDoor door)
        {
            Door = door;
        }

        public void InteractWith(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}