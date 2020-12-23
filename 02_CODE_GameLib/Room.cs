using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CODE_GameLib.Connections;
using CODE_GameLib.Doors;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; }
        public int Width { get; }
        public int Height { get; }
        public IImmutableList<IInteractable> Interactables => _interactables.ToImmutableList();
        public Player Player { get; set; }
        public IImmutableList<IDoor> Doors =>
            _hallways.Where(c => c.Door != null).Select(e => e.Door).ToImmutableList();

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
        /// Let the player leave the room and return the next room.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>next room id</returns>
        public Room Leave(Direction direction)
        {
            Player = null;

            return _hallways
                .Select(hallway => hallway.GetNextRoom(direction, Id))
                .FirstOrDefault(room => room != null);
        }

        public void Remove(IInteractable interactable) => _interactables.Remove(interactable);
    }
}