using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : IDoor
    {
        public bool IsOpen { get; private set; }
        public bool TriedToOpen { get; private set; }
        public bool Open(Player player)
        {
            TriedToOpen = true;
            
            return IsOpen;
        }

        public void Toggle() => IsOpen = !IsOpen;
    }
}