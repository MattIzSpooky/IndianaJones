using System.Drawing;
using System.Numerics;
using Console = Colorful.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public int RoomWidth { private get; set; }
        public int RoomHeight { private get; set; }
        public Vector2 PlayerPosition { private get; set; } 

        private const char WallIcon = '#';
        private const char PlayerIcon = 'X';

        private readonly Color _wallColor = Color.Yellow;
        private readonly Color _playerColor = Color.Blue;


        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            var rows = RoomHeight + 2;
            var columns = RoomWidth + 2;

            for (var y = 1; y <= rows; y++)
            {
                for (var x = 1; x <= columns; x++)
                {
                    if (y == 1 || y == rows) WriteWall();
                    else if (x == 1 || x == columns) WriteWall();
                    else
                    {
                        if (PlayerPosition.X + 2 == x && PlayerPosition.Y + 2 == y)
                        {
                            WritePlayer();
                        }
                        else
                        {
                            WriteEmpty();
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        private void WriteWall() =>  Console.Write(WallIcon, _wallColor);
        private void WritePlayer() =>  Console.Write(PlayerIcon, _playerColor);
        private void WriteEmpty() =>  Console.Write(" ");
    }
}