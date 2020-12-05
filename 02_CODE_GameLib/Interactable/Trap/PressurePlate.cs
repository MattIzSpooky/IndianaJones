namespace CODE_GameLib.Interactable.Trap
{
    public class PressurePlate : BaseInteractable
    {
        public PressurePlate(Room room, int x, int y) : base(room, x, y)
        {
        }

        public override void InteractWith(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}