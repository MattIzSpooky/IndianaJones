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
            if (other is not Player player) return false;

            var door = _hallway.Door;

            door?.Open(player);

            return door == null || door.IsOpen;
        }

        public override void InteractWith(Game gameContext, IInteractable other)
        {
            if (other is not Player player) return;

            var currentRoom = gameContext.CurrentRoom;
            var direction = player.LastDirection;

            var nextRoom = currentRoom.Leave(player.LastDirection);
            if (nextRoom == null) return;
            
            if (_hallway.Door == null) gameContext.PlayerEnterRoom(direction, nextRoom);
            else if (_hallway.Door.Open(player)) gameContext.PlayerEnterRoom(direction, nextRoom);
        }
    }
}