using System;
using System.Drawing;
using CODE_Frontend.ViewModels;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public RoomViewModel RoomViewModel { private get; set; }
        public HallwayViewModel[] Doors { private get; set; }
        public PlayerViewModel PlayerViewModel { private get; set; }
        public InteractableViewModel[] Interactables { private get; set; }

        private const char PlayerIcon = 'X';

        public GameView() : base(30, 30, "Temple of Doom - Game")
        {
        }

        public override void Draw()
        {
            ClearBuffer();

            WriteItems();
            WriteHallways();
            WritePlayer();
            WriteStats();

            WriteBuffer();
        }

        private void WriteHallways()
        {
            foreach (var door in Doors)
            {
                int x;
                int y;

                switch (door.Direction)
                {
                    case ViewableWindRose.North:
                        y = 0;
                        x = RoomViewModel.Width / 2;
                        break;
                    case ViewableWindRose.East:
                        y = RoomViewModel.Height / 2;
                        x = RoomViewModel.Width;
                        break;
                    case ViewableWindRose.South:
                        y = RoomViewModel.Height;
                        x = RoomViewModel.Width / 2;
                        break;
                    case ViewableWindRose.West:
                        y = RoomViewModel.Height / 2;
                        x = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Buffer[y][x] = CreateChar(door.Character, door.Color);
            }
        }

        private void WritePlayer()
        {
            var playerX = (int) PlayerViewModel.Position.X;
            var playerY = (int) PlayerViewModel.Position.Y;

            Buffer[playerY][playerX] = CreateChar(PlayerIcon, Color.Blue);
        }

        private void WriteItems()
        {
            if (Interactables == null) return;

            foreach (var item in Interactables)
            {
                var itemX = (int) item.Position.X;
                var itemY = (int) item.Position.Y;

                Buffer[itemY][itemX] = CreateChar(item.Character, item.Color);
            }
        }

        private void WriteStats()
        {
            StringCursor = RoomViewModel.Height + 1;

            WriteString($"Room: {RoomViewModel.Id}", Color.Fuchsia);
            WriteString($"Health: {PlayerViewModel.Lives}", Color.Crimson);
            WriteString($"Sankara stones: {PlayerViewModel.Score}", Color.Gold);
        }
    }
}