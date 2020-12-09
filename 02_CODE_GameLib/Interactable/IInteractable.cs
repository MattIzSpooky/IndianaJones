namespace CODE_GameLib.Interactable
{
    public interface IInteractable
    {
        public void InteractWith(Player player);
        public string GetType();
    }
}