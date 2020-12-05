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
        private IEnumerable<Connection> _connections;

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
        
        public void Enter(WindRose windRose)
        {
        }

        public void Remove(IInteractable interactable)
        {
        }
    }
}