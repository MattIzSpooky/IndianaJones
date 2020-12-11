namespace CODE_GameLib.Interactable.Doors
{
    public class ToggleDoor : IDoor
    {
        private bool _isOpen;

        public bool Open(Player player) => _isOpen;

        public void Toggle() => _isOpen = !_isOpen;
    }
}