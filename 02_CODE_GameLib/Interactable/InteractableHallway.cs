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
            var door = _hallway.Door;

            return door == null || door.IsOpen || !door.TriedToOpen;
        }

        public override void InteractWith(Game gameContext, IInteractable other)
        {
            if (other is not Player player) return;

            var currentRoom = gameContext.CurrentRoom;
            var direction = player.LastDirection;

            var canLeave = _hallway.Door == null || _hallway.Door.Open(player);
            
            if (!canLeave) return;
            
            var nextRoom = currentRoom.Leave(player.LastDirection);
            if (nextRoom == null) return;
            
            gameContext.PlayerEnterRoom(direction, nextRoom);
        }
    }
}