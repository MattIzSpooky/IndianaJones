using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Doors;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public IReadOnlyList<InteractableTile> InteractableTiles => _interactableTiles.AsReadOnly().ToList();

        // TODO: Ask to Ernst because view -> model connections?!!!!
        public IReadOnlyList<(WindRose, Tile)> Connections => _connections.Select(
            c =>
            {
                var direction = c.GetDirectionByRoom(Id);

                if (c.DoorContext != null) return (direction, c.DoorContext.Door.Tile);

                var tile = new Tile {Character = ' ', Color = Color.Empty};
                return (direction, tile);
            }).ToList().AsReadOnly();

        public Player Player { get; set; }
        private List<InteractableTile> _interactableTiles = new List<InteractableTile>();
        private List<Connection> _connections = new List<Connection>();

        public Room(int id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }

        public Room(int id, int width, int height, List<InteractableTile> interactableTiles) : this(id, width, height)
        {
            _interactableTiles = interactableTiles;
        }

        public void AddInteractableTile(InteractableTile tile)
        {
            _interactableTiles.Add(tile); // TODO: Update view.
        }

        public void SetConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public int Enter(WindRose windRose)
        {
            return _connections.Select(connection => connection.GetNextRoomId(windRose, Id))
                .FirstOrDefault(id => id != 0);
        }

        public void Remove(InteractableTile interactable)
        {
            _interactableTiles.Remove(interactable);
        }
    }
}