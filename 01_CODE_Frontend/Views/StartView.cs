using System.Drawing;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class StartView : ConsoleView
    {
        public StartView() : base(30, 30)
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
            WriteString(1, "JE MOEDER", Color.OrangeRed);
        }
    }
}