namespace CODE_GameLib.Interactable
{
    public class Wall : InteractableTile
    {
        public Wall(Room room, int x, int y) : base(room, x, y)
        {
        }
        public override bool AllowedToCollideWith(IInteractable other) => false;
        
        public override void InteractWith(Game gameContext, IInteractable other)
        {
        }
    }
}