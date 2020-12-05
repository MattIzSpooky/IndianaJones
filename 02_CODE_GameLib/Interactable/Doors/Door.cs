namespace CODE_GameLib.Interactable.Doors
{
    public class Door : IInteractable
    {
        private IDoor _door;

        public Door(IDoor door)
        {
            _door = door;
        }

        public void InteractWith(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}