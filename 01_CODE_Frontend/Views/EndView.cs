using System.Drawing;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class EndView : ConsoleView
    {
        public int PlayerHealth { private get; set; }
        public int StonesCollected { private get; set; }

        public EndView() : base(30, 30)
        {
        }

        public override void Draw()
        {
            ClearBuffer();

            WriteStats();
            WriteInstructions();
            
            WriteBuffer();
        }

        private void WriteStats()
        {
            WriteString(0, "Game ended!", Color.White);
            WriteString(1, $"Player had: {PlayerHealth} HP left", Color.Red);
            WriteString(2, $"Player had collected {StonesCollected} stones", Color.Yellow);
        }

        private void WriteInstructions()
        {
            WriteString(5, $"To exit press Esc or Space", Color.Fuchsia);
            WriteString(6, $"Press R to restart", Color.Gold);
        }
    }
}