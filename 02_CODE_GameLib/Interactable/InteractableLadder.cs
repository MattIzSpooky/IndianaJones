using CODE_GameLib.Connections;

namespace CODE_GameLib.Interactable
{
    public class InteractableLadder : InteractableTile
    {
        private readonly Ladder _ladder;
        
        public InteractableLadder OtherSide { private get; set; }
        
        public InteractableLadder(Room room, int x, int y, Ladder ladder) : base(room, x, y)
        {
            _ladder = ladder;
        }
        public override void InteractWith(Game context, IInteractable other)
        {
            var currentRoom = context.CurrentRoom;
            var nextRoom = _ladder.Climb(currentRoom);
            
            context.PlayerEnterRoom(nextRoom, OtherSide.X, OtherSide.Y);
        }
    }
}