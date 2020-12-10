namespace CODE_GameLib.Interactable.Doors
{
    public class DoorContext : IInteractable
    {
        public IDoor Door { get; }

        public DoorContext(IDoor door)
        {
            Door = door;
        }

        public bool CanInteractWith(IInteractable other)
        {
            return other is Player;
        }

        public void InteractWith(IInteractable other)
        {
            throw new System.NotImplementedException();
        }
    }
}