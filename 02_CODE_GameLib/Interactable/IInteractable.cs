namespace CODE_GameLib.Interactable
{
    public interface IInteractable : IPosition
    {
        public void InteractWith(Player player);
    }
}