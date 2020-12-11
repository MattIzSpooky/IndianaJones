namespace CODE_GameLib.Interactable.Doors
{
    public interface IDoor
    {
        public bool Open(Player player);
        
        public Tile Tile { get; }
    }
}