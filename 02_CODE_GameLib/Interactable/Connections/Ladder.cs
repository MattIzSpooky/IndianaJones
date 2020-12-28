namespace CODE_GameLib.Interactable.Connections
{
    public class Ladder : InteractableTile
    {
        public Ladder OtherSide { private get; set; }
        private readonly Room _otherRoom;
        
        public Ladder(Room room, int x, int y, Room otherRoom) : base(room, x, y)
        {
            _otherRoom = otherRoom;
        }
        public override void InteractWith(Game context, IInteractable other)
        {
            Room.Remove(other); // Leave the room by removing yourself from it.
            context.PlayerEnterRoom(_otherRoom, OtherSide.X, OtherSide.Y);
        }
    }
}