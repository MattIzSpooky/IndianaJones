using System.Drawing;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class StartView : ConsoleView
    {
        public StartView() : base(30, 30, "Temple of Doom - Start")
        {
        }

        public override void Draw()
        {
            ClearBuffer();
            
            WriteWelcome();
            
            WriteBuffer();
        }

        private void WriteWelcome()
        {
            WriteString("Welcome to the temple of doom!", Color.OrangeRed);
            WriteString("Press Space to continue..", Color.MediumVioletRed);
            WriteString("Press Esc to exit..", Color.Goldenrod);
        }
    }
}