using System;
using System.Collections.Generic;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        // TODO: Maybe add a current room?

        public ConsoleKey KeyPressed { get; private set; }
        public bool Quit { get; private set; } = false;

        public Room Room
        {
            get => default;
            set
            {
            }
        }

        public Player Player
        {
            get => default;
            set
            {
            }
        }

        private IEnumerable<Room> _rooms;
        private Player _player;

        public Game(IEnumerable<Room> rooms, Player player)
        {
            _rooms = rooms;
            _player = player;
        }

        public void Run()
        {
            KeyPressed = Console.ReadKey().Key;
            Quit = KeyPressed == ConsoleKey.Escape;

            while (!Quit)
            {
                Updated?.Invoke(this, this);

                KeyPressed = Console.ReadKey().Key;
                Quit = KeyPressed == ConsoleKey.Escape;
            }

            Updated?.Invoke(this, this);
        }
    }
}
