using System.Drawing;
using CODE_Frontend.ViewModels;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class EndView : ConsoleView
    {
        public PlayerViewModel PlayerViewModel { private get; set; }

        public EndView() : base(30, 30, "Temple of Doom - End")
        {
        }

        public override void Draw()
        {
            ClearBuffer();

            WriteMessage();
            WriteStats();
            WriteInstructions();

            WriteBuffer();
        }

        private void WriteMessage()
        {
            if (PlayerViewModel.Lives != 0) WriteString("Congratulations!", Color.Magenta);
            else WriteString("You died!", Color.Red);
            
            StringCursor++;
        }

        private void WriteStats()
        {
            WriteString($"Stats:", Color.Aqua);
            WriteString($"Player had: {PlayerViewModel.Lives} HP left", Color.Red);
            WriteString($"Player had collected {PlayerViewModel.Score} stones", Color.Yellow);
        }

        private void WriteInstructions()
        {
            StringCursor++;
            WriteString("To exit press Esc or Space", Color.Fuchsia);
            WriteString("Press R to restart", Color.Gold);
        }
    }
}