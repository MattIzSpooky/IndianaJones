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
        private IEnumerable<BaseInteractable> _collectables;
        private IEnumerable<Connection> _connections;

        public void Enter(WindRose windRose)
        {
        }

        public void Remove(BaseInteractable interactable)
        {
        }
    }
}