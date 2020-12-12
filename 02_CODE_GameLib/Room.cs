using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Doors;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; }
        public int Width { get; }
        public int Height { get; }
        public IImmutableList<InteractableTile> InteractableTiles => _interactableTiles.ToImmutableList();
        public IImmutableList<Hallway> Hallways => _hallways.ToImmutableList();
        public Player Player { get; set; }

        public IImmutableList<DoorContext> Doors =>
            _hallways.Where(c => c.DoorContext != null).Select(e => e.DoorContext).ToImmutableList();

        private readonly List<InteractableTile> _interactableTiles = new List<InteractableTile>();
        private readonly List<Hallway> _hallways = new List<Hallway>();

        public Room(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }

        public void AddInteractableTile(InteractableTile tile) => _interactableTiles.Add(tile);
        public void AddHallway(Hallway hallway) => _hallways.Add(hallway);

        /// <summary>
        /// Let the player leave the room and return the next room id
        /// </summary>
        /// <param name="windRose"></param>
        /// <returns>next room id</returns>
        public int Leave(WindRose windRose)
        {
            Player = null;

            return _hallways
                .Select(hallway => hallway.GetNextRoomId(windRose, Id))
                .FirstOrDefault(id => id != 0);
        }

        public Hallway GetHallWayByDirection(WindRose direction) =>
            _hallways.Find(e => e.GetDirectionByRoom(Id) == direction);

        public void Remove(InteractableTile interactable) => _interactableTiles.Remove(interactable);
    }
}