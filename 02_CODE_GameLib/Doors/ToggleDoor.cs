using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : IDoor
    {
        private bool _isOpen;

        public bool Open(Player player) => _isOpen;

        public void Toggle() => _isOpen = !_isOpen;
    }
}