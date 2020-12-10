namespace CODE_GameLib.Interactable
{
    public interface IInteractable
    {
        public bool CanInteractWith(IInteractable other);
        public void InteractWith(IInteractable other);
    }
}