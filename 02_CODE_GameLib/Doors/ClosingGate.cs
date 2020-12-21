using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public class ClosingGate : IDoor
    {
        public bool IsOpen { get; private set; } = true;
        public bool TriedToOpen { get; private set; }

        public bool Open(Player player)
        {
            TriedToOpen = true;
            
            if (!IsOpen) return IsOpen;
            
            IsOpen = false;
            
            return true;
        }
    }
}