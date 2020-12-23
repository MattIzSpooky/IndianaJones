using System.Collections.Immutable;

namespace CODE_GameLib.Interactable
{
    /// <summary>
    /// Allows objects to interact with each other
    /// </summary>
    public interface IInteractable : IPosition
    {
        /// <summary>
        /// Checks if the object collides with this object.
        /// </summary>
        /// <param name="other">The object it wants to collide with</param>
        /// <returns>bool</returns>
        public bool CollidesWith(IInteractable other) => other.X == X && other.Y == Y;

        /// <summary>
        /// Checks if the objects are allowed to collide with each other.
        /// </summary>
        /// <param name="cheats">A dictionary containing all of the cheats</param>
        /// <param name="other">The object it wants to collide with</param>
        /// <returns>bool</returns>
        public bool AllowedToCollideWith(ImmutableDictionary<Cheat, bool> cheats, IInteractable other);

        /// <summary>
        /// The action that happens once the objects collide.
        /// The game object is passed so that we can manipulate the Game from within an object, let's say we want to change rooms.
        /// </summary>
        /// <param name="gameContext">The game object.</param>
        /// <param name="other">The object it wants to interact with</param>
        /// <returns>bool</returns>
        public void InteractWith(Game gameContext, IInteractable other);
    }
}