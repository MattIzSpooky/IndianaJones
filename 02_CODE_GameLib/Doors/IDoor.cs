using CODE_GameLib.Interactable;

namespace CODE_GameLib.Doors
{
    public interface IDoor
    {
        public bool IsOpen { get; }
        public bool TriedToOpen { get; }
        public bool Open(Player player);
    }
}