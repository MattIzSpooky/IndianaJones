using System.Collections.Generic;
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

        public Vector2[] Items { private get; set; }

        private const char WallIcon = '#';
        private const char PlayerIcon = 'X';
        private const char ItemIcon = 'I';
        private const int WallOffset = 1;

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            var rows = RoomHeight + WallOffset;
            var columns = RoomWidth + WallOffset;

            for (var y = 0; y <= rows; y++)
            {
                for (var x = 0; x <= columns; x++)
                {
                    if (y == 0 || y == rows) WriteWall();
                    else if (x == 0 || x == columns) WriteWall();
                    else WriteEmpty();
                }

                Console.WriteLine();
            }

            WritePlayer();
            WriteItems();
            
            RenderDebug();
        }

        private void WriteItems()
        {
            foreach (var item in Items)
            {
                var itemX = (int) (item.X + WallOffset);
                var itemY = (int) (item.Y + WallOffset);

                Console.SetCursorPosition(itemX, itemY);

                WriteItem();
            }
        }

        private void RenderDebug()
        {
            Console.SetCursorPosition(0, 20);

            Console.WriteLine($"({PlayerPosition.X},{PlayerPosition.Y})");
        }

        private void WriteWall() => Console.Write(WallIcon, Color.Yellow);
        private void WriteItem() => Console.Write(ItemIcon, Color.Red);

        private void WritePlayer()
        {
            var playerX = (int) (PlayerPosition.X + WallOffset);
            var playerY = (int) (PlayerPosition.Y + WallOffset);

            Console.SetCursorPosition(playerX, playerY);
            Console.Write(PlayerIcon, Color.Blue);
        }

        private void WriteEmpty() => Console.Write(" ");
    }
}