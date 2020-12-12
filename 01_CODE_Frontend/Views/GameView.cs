using System;
using System.Drawing;
using CODE_Frontend.ViewModels;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public ViewableRoom Room { private get; set; }
        public ViewableHallway[] Doors { private get; set; }
        public ViewablePlayer Player { private get; set; }
        public ViewableInteractable[] Interactables { private get; set; }

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
                        x = Room.Width / 2;
                        break;
                    case ViewableWindRose.East:
                        y = Room.Height / 2;
                        x = Room.Width;
                        break;
                    case ViewableWindRose.South:
                        y = Room.Height;
                        x = Room.Width / 2;
                        break;
                    case ViewableWindRose.West:
                        y = Room.Height / 2;
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
            var playerX = (int) Player.Position.X;
            var playerY = (int) Player.Position.Y;

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
            StringCursor = Room.Height + 1;

            WriteString($"Room: {Room.Id}", Color.Fuchsia);
            WriteString($"Health: {Player.Lives}", Color.Crimson);
            WriteString($"Sankara stones: {Player.Score}", Color.Gold);
        }
    }
}