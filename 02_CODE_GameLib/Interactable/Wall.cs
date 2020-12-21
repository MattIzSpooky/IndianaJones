namespace CODE_GameLib.Interactable
{
    public class Wall : InteractableTile
    {
        public Wall(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(IInteractable other)
        {
        }
    }
}