using System;

namespace CODE_GameLib.Interactable.Doors
{
    public class DoorFactory
    {
        private const string Colored = "colored";
        private const string Toggle = "toggle";
        private const string ClosingGate = "closing gate";

        public IDoor Create(string type, string args)
        {
            return type switch
            {
                Colored => new ColoredDoor(args),
                Toggle => new ToggleDoor(),
                ClosingGate => new ClosingGate(),
                _ => throw new ArgumentException($"Type: {type} is not valid")
            };
        }
    }
}