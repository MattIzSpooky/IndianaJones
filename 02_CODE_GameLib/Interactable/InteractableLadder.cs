using System.Linq;
using CODE_GameLib.Connections;

namespace CODE_GameLib.Interactable
{
    public class InteractableLadder : InteractableTile
    {
        private readonly Ladder _ladder;
        
        public InteractableLadder(Room room, int x, int y, Ladder ladder) : base(room, x, y)
        {
            _ladder = ladder;
        }
        public override void InteractWith(Game gameContext, IInteractable other)
        {
            var currentRoom = gameContext.CurrentRoom;
            var nextRoom = _ladder.Climb(currentRoom);
            var otherSide = nextRoom.Interactables.First(a => a is InteractableLadder);
            
            gameContext.PlayerEnterRoom(nextRoom, otherSide.X, otherSide.Y);
        }
    }
}