using System.Collections.Generic;
using CODE_GameLib.Interactable;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Player _player;
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
        
        public void Enter(WindRose windRose)
        {
        }

        public void Remove(IInteractable interactable)
        {
        }
    }
}