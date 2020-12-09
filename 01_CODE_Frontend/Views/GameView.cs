using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
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

        private long frames = 0;

        private char[][] buffer = null;

        public GameView()
        {
            buffer = CreateBuffer();
        }

        public override void Draw()
        {
            frames++;

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            ClearBuffer();
            WriteWalls();

            Console.Clear();
            
            for(var y=0; y<50; ++y)
                Console.WriteLine(buffer[y]);
            
            // WriteWalls();
            // WritePlayer();
            // WriteItems();
            //
            // RenderDebug();
            
            Thread.Sleep(16);
        }

        private char[][] CreateBuffer()
        {
            var height = 50;
            var width = 50;

            var render = new char[height][];
            for (var y = 0; y < width; ++y)
                render[y] = new char[width];

            return render;
        }

        private void ClearBuffer()
        {
            for (var y = 0; y < 50; ++y)
            for (var x = 0; x < 50; ++x)
                buffer[y][x] = ' ';
        }

        private void WriteWalls()
        {
            var rows = RoomHeight + WallOffset;
            var columns = RoomWidth + WallOffset;
            
            for (var y = 0; y <= rows; y++)
            {
                for (var x = 0; x <= columns; x++)
                {
                    if (y == 0 || y == rows) buffer[y][x] = WallIcon;
                    else if (x == 0 || x == columns) buffer[y][x] = WallIcon;
                }
            }
        }

        // private void WriteWalls()
        // {
        //     var rows = RoomHeight + WallOffset;
        //     var columns = RoomWidth + WallOffset;
        //
        //     for (var y = 0; y <= rows; y++)
        //     {
        //         for (var x = 0; x <= columns; x++)
        //         {
        //             Console.SetCursorPosition(x, y);
        //
        //             if (y == 0 || y == rows) WriteWall();
        //             else if (x == 0 || x == columns) WriteWall();
        //         }
        //
        //         Console.WriteLine();
        //     }
        // }
        //
        // private void WriteItems()
        // {
        //     if (Items == null) return;
        //
        //     foreach (var item in Items)
        //     {
        //         var itemX = (int) (item.X + WallOffset);
        //         var itemY = (int) (item.Y + WallOffset);
        //
        //         Console.SetCursorPosition(itemX, itemY);
        //
        //         WriteItem();
        //     }
        // }
        //
        // private void RenderDebug()
        // {
        //     Console.SetCursorPosition(0, 20);
        //
        //     Console.WriteLine($"Player: ({PlayerPosition.X},{PlayerPosition.Y})");
        //     Console.WriteLine($"Frames: {frames}");
        //
        //     if (Items != null)
        //     {
        //         foreach (var item in Items)
        //         {
        //             Console.WriteLine($"Item: ({item.X},{item.Y})");
        //         }
        //     }
        // }
        //
        // private void WriteWall() => Console.Write(WallIcon, Color.Yellow);
        // private void WriteItem() => Console.Write(ItemIcon, Color.Red);
        //
        // private void WritePlayer()
        // {
        //     var playerX = (int) (PlayerPosition.X + WallOffset);
        //     var playerY = (int) (PlayerPosition.Y + WallOffset);
        //
        //     Console.SetCursorPosition(playerX, playerY);
        //
        //     Console.Write(PlayerIcon, Color.Blue);
        // }
        //
        // private void WriteEmpty() => Console.Write(" ");
    }
}