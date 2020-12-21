using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : IDoor
    {
        public bool IsOpen { get; private set; }
        public bool Open(Player player) => IsOpen;

        public void Toggle() => IsOpen = !IsOpen;
    }
}