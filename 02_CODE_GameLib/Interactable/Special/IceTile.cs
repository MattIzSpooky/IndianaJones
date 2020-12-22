namespace CODE_GameLib.Interactable.Special
{
    public class IceTile : InteractableTile
    {
        public IceTile(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(Game gameContext, IInteractable other)
        {
            var player = gameContext.Player;
            gameContext.MovePlayer(player.LastDirection);
        }
    }
}