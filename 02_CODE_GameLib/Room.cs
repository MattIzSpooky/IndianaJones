using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CODE_GameLib.Connections;
using CODE_GameLib.Doors;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Enemies;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; }
        public int Width { get; }
        public int Height { get; }
        public IImmutableList<IInteractable> Interactables => _interactables.ToImmutableList();

        public IImmutableList<IDoor> Doors =>
            _hallways.Where(c => c.Door != null).Select(e => e.Door).ToImmutableList();

        public IImmutableList<InteractableEnemy> Enemies =>
            _interactables.Where(i => i is InteractableEnemy).Cast<InteractableEnemy>().ToImmutableList();

        private readonly List<IInteractable> _interactables = new List<IInteractable>();
        private readonly List<Hallway> _hallways = new List<Hallway>();

        public Room(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }

        public void AddInteractable(IInteractable interactable) => _interactables.Add(interactable);
        public void AddHallway(Hallway hallway) => _hallways.Add(hallway);

        /// <summary>
        /// Let an interactable leave from a direction, return next room based on direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="interactable"></param>
        /// <returns>next room id</returns>
        public Room Leave(Direction direction, IInteractable interactable)
        {
            Remove(interactable);

            return _hallways
                .Select(hallway => hallway.GetNextRoom(direction, Id))
                .FirstOrDefault(room => room != null);
        }

        public void MoveMovables()
        {
            var movables = _interactables.Where(i => i is IMovable).Cast<IMovable>();

            foreach (var movable in movables)
            {
                movable.Move();
            }
        }

        public void Remove(IInteractable interactable) => _interactables.Remove(interactable);
    }
}