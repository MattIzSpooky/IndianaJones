namespace CODE_GameLib.Interactable
{
    /// <summary>
    /// Allows objects to interact with each other
    /// </summary>
    public interface IInteractable : IPosition
    {
        /// <summary>
        /// Checks if object can interact with each other.
        /// </summary>
        /// <param name="other">The object it wants to interact with</param>
        /// <returns>bool</returns>
        public bool CollidesWith(IInteractable other);
        public void InteractWith(IInteractable other);
    }
}