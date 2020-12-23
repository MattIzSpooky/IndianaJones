using System;
using System.Drawing;
using System.Linq;
using CODE_Frontend.ViewModels;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public RoomViewModel Room { private get; set; }
        public PlayerViewModel Player { private get; set; }
        public InteractableViewModel[] Interactables { private get; set; }
        public ViewableCheat[] EnabledCheats { private get; set; }

        private const char PlayerIcon = 'X';

        public GameView() : base(30, 30, "Temple of Doom - Game")
        {
        }

        public override void Draw()
        {
            ClearBuffer();

            WriteInteractables();
            WritePlayer();
            WriteStats();

            WriteBuffer();
        }

        private void WritePlayer()
        {
            var playerX = (int) Player.Position.X;
            var playerY = (int) Player.Position.Y;

            Buffer[playerY][playerX] = CreateChar(PlayerIcon, Color.Blue);
        }

        private void WriteInteractables()
        {
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
            WriteString($"Cheats: {string.Join(",", EnabledCheats)}", Color.Orange);
        }
    }
}