namespace CODE_GameLib.Interactable.Doors
{
    /// <summary>
    /// Forwards the functionalities of IDoor
    ///
    /// see strategy pattern (https://refactoring.guru/design-patterns/strategy)
    /// </summary>
    public class DoorContext
    {
        public IDoor Door { get; }

        public DoorContext(IDoor door)
        {
            Door = door;
        }

        public bool Open(Player player) => Door.Open(player);
    }
}