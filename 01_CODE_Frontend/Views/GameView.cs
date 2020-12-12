﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using MVC.Views.Console;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public int RoomWidth { private get; set; }
        public int RoomHeight { private get; set; }
        public ViewableHallway[] Doors { private get; set; }
        public Vector2 PlayerPosition { private get; set; }
        public int PlayerHealth { private get; set; }
        public ViewableInteractable[] Interactables { private get; set; }

        private double _frameTime;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private const char PlayerIcon = 'X';

        public GameView() : base(30, 30, "Temple of Doom - Game")
        {
        }

        public override void Draw()
        {
            _stopwatch.Start();

            ClearBuffer();

            WriteItems();
            WriteDoors();
            WritePlayer();

            RenderDebug();

            WriteBuffer();

            _stopwatch.Stop();
            _frameTime = _stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Reset();
        }

        private void WriteDoors()
        {
            foreach (var door in Doors)
            {
                int x;
                int y;

                switch (door.Direction)
                {
                    case ViewableWindRose.North:
                        y = 0;
                        x = RoomWidth / 2;
                        break;
                    case ViewableWindRose.East:
                        y = RoomHeight / 2;
                        x = RoomWidth;
                        break;
                    case ViewableWindRose.South:
                        y = RoomHeight;
                        x = RoomWidth / 2;
                        break;
                    case ViewableWindRose.West:
                        y = RoomHeight / 2;
                        x = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Buffer[y][x] = CreateChar(door.Character, door.Color);
            }
        }

        private void WritePlayer()
        {
            var playerX = (int) (PlayerPosition.X);
            var playerY = (int) (PlayerPosition.Y);

            Buffer[playerY][playerX] = CreateChar(PlayerIcon, Color.Blue);
        }

        private void WriteItems()
        {
            if (Interactables == null) return;

            foreach (var item in Interactables)
            {
                var itemX = (int) (item.Position.X);
                var itemY = (int) (item.Position.Y);

                Buffer[itemY][itemX] = CreateChar(item.Character, item.Color);
            }
        }

        private void RenderDebug()
        {
            WriteString(15, $"Player position: ({PlayerPosition.X}, {PlayerPosition.Y})", Color.Fuchsia);
            WriteString(16, $"Room dimentions: W={RoomWidth}, H={RoomHeight}", Color.Gold);
            WriteString(17, $"Player health: {PlayerHealth}", Color.Crimson);
            WriteString(18, $"Frame time: {_frameTime}", Color.Lime);
        }
    }
}