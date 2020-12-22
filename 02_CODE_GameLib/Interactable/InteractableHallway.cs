using CODE_GameLib.Connections;

namespace CODE_GameLib.Interactable
{
    public class InteractableHallway : InteractableTile
    {
        private readonly Hallway _hallway;

        public InteractableHallway(Room room, int x, int y, Hallway hallway) : base(room, x, y)
        {
            _hallway = hallway;
        }

        public override bool AllowedToCollideWith(IInteractable other)
        {
            if (!(other is Player player)) return false;
            
            var door = _hallway.Door;

            if (door == null) return true;
            if (!door.TriedToOpen && door.IsOpen) return true;

            return door.Open(player);
        }

        public override void InteractWith(Game gameContext, IInteractable other)
        {
            if (!(other is Player player)) return;

            var currentRoom = gameContext.CurrentRoom;
            var direction = player.LastDirection;

            var canLeave = _hallway.Door == null || _hallway.Door.Open(player);
            
            if (!canLeave) return;
            
            var nextRoom = currentRoom.Leave(direction);
            if (nextRoom == null) return;
            
            gameContext.PlayerEnterRoom(direction, nextRoom);
        }
    }
}