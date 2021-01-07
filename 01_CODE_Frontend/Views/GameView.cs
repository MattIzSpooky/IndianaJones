using System.Drawing;
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
            WriteMiscInfo();

            WriteBuffer();
        }

        private void WritePlayer()
        {
            var playerX = Player.Position.X;
            var playerY = Player.Position.Y;

            Buffer[playerY][playerX] = CreateChar(PlayerIcon, Color.Blue);
        }

        private void WriteInteractables()
        {
            foreach (var item in Interactables)
            {
                var itemX = item.Position.X;
                var itemY = item.Position.Y;

                Buffer[itemY][itemX] = CreateChar(item.Character, item.Color);
            }
        }

        private void WriteMiscInfo()
        {
            StringCursor = Room.Height + 1;

            WriteString($"Room: {Room.Id}", Color.Fuchsia);
            WriteString($"Health: {Player.Lives}", Color.Crimson);
            WriteString($"Sankara stones: {Player.Score}", Color.Gold);
            WriteString("Cheats: ", Color.Orange);
            
            foreach (var cheat in EnabledCheats)
            {
                WriteString($" - {cheat}", Color.Orange);
            }
        }
    }
}