using System.Drawing;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class EndView : ConsoleView
    {
        public int PlayerHealth { private get; set; }
        public int StonesCollected { private get; set; }

        public EndView() : base(30, 30, "Temple of Doom - End")
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
            WriteString("Game ended!", Color.White);
            WriteString($"Player had: {PlayerHealth} HP left", Color.Red);
            WriteString($"Player had collected {StonesCollected} stones", Color.Yellow);
        }

        private void WriteInstructions()
        {
            WriteString("To exit press Esc or Space", Color.Fuchsia);
            WriteString("Press R to restart", Color.Gold);
        }
    }
}