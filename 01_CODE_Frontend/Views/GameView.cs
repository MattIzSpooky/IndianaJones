using System.Drawing;
using Console = Colorful.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public int RoomWidth { private get; set; }
        public int RoomHeight { private get; set; }

        private const char WallIcon = '#';


        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            var rows = RoomHeight + 2;
            var columns = RoomWidth + 2;

            for (var i = 1; i <= rows; i++)
            {
                for (var j = 1; j <= columns; j++)
                {
                    if (i == 1 || i == rows) Console.Write(WallIcon, Color.Yellow);
                    else if (j == 1 || j == columns) Console.Write(WallIcon, Color.Yellow);
                    else Console.Write(" ", Color.Green);
                }

                Console.WriteLine();
            }
        }
    }
}