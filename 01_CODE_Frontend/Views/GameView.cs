using System;
using System.Collections.Generic;
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

        private const int Height = 30;
        private const int Width = 30;

        private long frames = 0;

        private char[][] buffer;

        // private Dictionary<char, Color> _colors = new Dictionary<char, Color>()
        // {
        //     {WallIcon, Color.Yellow},
        //     {PlayerIcon, Color.Blue},
        // };

        public GameView()
        {
            buffer = CreateBuffer();
        }

        public override void Draw()
        {
            frames++;

            ClearBuffer();

            WriteWalls();
            WriteItems();
            WritePlayer();

            RenderDebug();

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < buffer[y].Length; x++)
                {
                    var character = buffer[y][x];

                    // TODO: Find a faster solution for colors.
                    // _colors.TryGetValue(character, out var color);
                    // if (color == Color.Empty) color = Color.White;
                    //
                    // Console.Write(character, color);
                    
                    Console.Write(character);
                }

                Console.WriteLine();
            }
        }

        private char[][] CreateBuffer()
        {
            var render = new char[Height][];

            for (var y = 0; y < Width; ++y)
                render[y] = new char[Width];

            return render;
        }

        private void ClearBuffer()
        {
            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Width; ++x)
                {
                    buffer[y][x] = ' ';
                }
            }
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

        private void WritePlayer()
        {
            var playerX = (int) (PlayerPosition.X + WallOffset);
            var playerY = (int) (PlayerPosition.Y + WallOffset);

            buffer[playerY][playerX] = PlayerIcon;
        }

        private void WriteItems()
        {
            if (Items == null) return;

            foreach (var item in Items)
            {
                var itemX = (int) (item.X + WallOffset);
                var itemY = (int) (item.Y + WallOffset);

                buffer[itemY][itemX] = ItemIcon;
            }
        }

        private void RenderDebug()
        {
            buffer[24][0] = PlayerIcon;
            buffer[24][1] = ':';
            
            var playerPosX = PlayerPosition.X.ToString().ToCharArray();
            
            for (var i = 0; i < playerPosX.Length; i++)
            {
                buffer[24][i + 2] = playerPosX[i];
            }

            /*buffer[23][0] = 'F';
            buffer[23][1] = ':';
            
            var frameArr = frames.ToString().ToCharArray();

            for (var i = 0; i < frameArr.Length; i++)
            {
                buffer[23][i + 2] = frameArr[i];
            }*/
        }
    }
}