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
        public IImmutableList<Hallway> Hallways => _connections.ToImmutableList();
        public Player Player { get; set; }
        
        private readonly List<InteractableTile> _interactableTiles = new List<InteractableTile>();
        private readonly List<Hallway> _connections = new List<Hallway>();

        public IReadOnlyList<DoorContext> GetDoors() =>
            _connections.Where(c => c.DoorContext != null).Select(e => e.DoorContext).ToImmutableList();

        public Room(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }

        public void AddInteractableTile(InteractableTile tile) => _interactableTiles.Add(tile);
        public void SetConnection(Hallway hallway) => _connections.Add(hallway);

        public int Leave(WindRose windRose)
        {
            Player = null;

            return _connections
                .Select(connection => connection.GetNextRoomId(windRose, Id))
                .FirstOrDefault(id => id != 0);
        }

        public Hallway GetHallWayByDirection(WindRose direction) =>
            _connections.Find(e => e.GetDirectionByRoom(Id) == direction);

        public void Remove(InteractableTile interactable) => _interactableTiles.Remove(interactable);
    }
}