﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using CODE_GameLib;
using Console = Colorful.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public int RoomWidth { private get; set; }
        public int RoomHeight { private get; set; }
        public Vector2 PlayerPosition { private get; set; }
        public int PlayerHealth { private get; set; }
        public ViewableItem[] Items { private get; set; }

        private double frames;

        private Stopwatch _stopwatch = new Stopwatch();

        private const char WallIcon = '#';
        private const char PlayerIcon = 'X';
        private const char ItemIcon = 'I';
        private const char KeyIcon = 'K';
        private const char SankaraStoneIcon = 'S';
        private const char BoobyTrapIcon = 'O';
        private const char DisappearingBoobyTrapIcon = '@';
        private const char PressurePlateIcon = 'T';

        private const int WallOffset = 1;

        public GameView() : base(30, 30)
        {
        }

        public override void Draw()
        {
            _stopwatch.Start();

            ClearBuffer();

            WriteWalls();
            WriteItems();
            WritePlayer();

            RenderDebug();

            WriteBuffer();

            _stopwatch.Stop();
            frames = _stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Reset();
        }

        private void WriteWalls()
        {
            var rows = RoomHeight + WallOffset;
            var columns = RoomWidth + WallOffset;

            for (var y = 0; y <= rows; y++)
            {
                for (var x = 0; x <= columns; x++)
                {
                    if (y == 0 || y == rows)
                    {
                        Buffer[y][x] = CreateChar(WallIcon, Color.Yellow);
                    }
                    else if (x == 0 || x == columns)
                    {
                        Buffer[y][x] = CreateChar(WallIcon, Color.Yellow);
                    }
                }
            }
        }

        private void WritePlayer()
        {
            var playerX = (int) (PlayerPosition.X + WallOffset);
            var playerY = (int) (PlayerPosition.Y + WallOffset);

            Buffer[playerY][playerX] = CreateChar(PlayerIcon, Color.Blue);
        }

        private void WriteItems()
        {
            if (Items == null) return;

            foreach (var item in Items)
            {
                var itemX = (int) (item.Position.X + WallOffset);
                var itemY = (int) (item.Position.Y + WallOffset);

                var icon = item.Type switch
                {
                    "SankaraStone" => CreateChar(SankaraStoneIcon, Color.Orange),
                    "Key" => CreateChar(KeyIcon, Color.Chartreuse),
                    "BoobyTrap" => CreateChar(BoobyTrapIcon),
                    "DisappearingBoobyTrap" => CreateChar(DisappearingBoobyTrapIcon),
                    "PressurePlate" => CreateChar(PressurePlateIcon),
                    _ => CreateChar(ItemIcon), // TODO: Should be a non-reserved icon.
                };

                Buffer[itemY][itemX] = icon;
            }
        }

        private void RenderDebug()
        {
            Buffer[24][0] = CreateChar(PlayerIcon);
            Buffer[24][1] = CreateChar(':');

            var playerPosX = PlayerPosition.X.ToString().ToCharArray();

            for (var i = 0; i < playerPosX.Length; i++)
            {
                Buffer[24][i + 2] = CreateChar(playerPosX[i], Color.Fuchsia);
            }

            Buffer[23][0] = CreateChar('H', Color.OrangeRed);
            Buffer[23][1] = CreateChar('P', Color.OrangeRed);
            Buffer[23][2] = CreateChar(':');

            var playerHealth = PlayerHealth.ToString().ToCharArray();

            for (var i = 0; i < playerHealth.Length; i++)
            {
                Buffer[23][i + 3] = CreateChar(playerHealth[i], Color.Fuchsia);
            }

            Buffer[22][0] = CreateChar('F', Color.Lime);
            Buffer[22][1] = CreateChar('T', Color.Lime);
            Buffer[22][2] = CreateChar(':');

            var frameArr = frames.ToString().ToCharArray();

            for (var i = 0; i < frameArr.Length; i++)
            {
                Buffer[22][i + 3] = CreateChar(frameArr[i], Color.Fuchsia);
            }
        }
    }
}