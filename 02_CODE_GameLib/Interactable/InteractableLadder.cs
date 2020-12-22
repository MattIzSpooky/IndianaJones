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
        public override void InteractWith(Game gameContext, IInteractable other)
        {
            var currentRoom = gameContext.CurrentRoom;
            var nextRoom = _ladder.Climb(currentRoom);
            
            gameContext.PlayerEnterRoom(nextRoom, OtherSide.X, OtherSide.Y);
        }
    }
}