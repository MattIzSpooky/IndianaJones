using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Colorful;

namespace CODE_Frontend.Views
{
    public class GameView : ConsoleView
    {
        public int RoomWidth { private get; set; }
        public int RoomHeight { private get; set; }
        public Vector2 PlayerPosition { private get; set; }
        public int PlayerHealth { private get; set; }
        public ViewableItem[] Items { private get; set; }

        private const char WallIcon = '#';
        private const char PlayerIcon = 'X';
        private const char ItemIcon = 'I';
        private const char KeyIcon = 'K';
        private const char SankaraStoneIcon = 'S';
        private const char BoobyTrapIcon = 'O';
        private const char DisappearingBoobyTrapIcon = '@';
        private const char PressurePlateIcon = 'T';
        
        
        private const int WallOffset = 1;

        private Dictionary<char, Color> _colors = new Dictionary<char, Color>()
        {
            {WallIcon, Color.Yellow},
            {PlayerIcon, Color.Blue},
            {SankaraStoneIcon, Color.Orange},
        };

        public GameView() : base(30, 30)
        {
        }

        public override void Draw()
        {
            ClearBuffer();

            WriteWalls();
            WriteItems();
            WritePlayer();

            RenderDebug();
            
            WriteBuffer();
        }

        protected override void WriteBuffer()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Buffer[y].Length; x++)
                {
                   var character = Buffer[y][x];
                    _colors.TryGetValue(character, out var color);
                    if (color.IsEmpty) color = Color.White;
                    
                   Console.Write(character, color);
                   
                }
                Console.WriteLine();
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
                    if (y == 0 || y == rows) Buffer[y][x] = WallIcon;
                    else if (x == 0 || x == columns) Buffer[y][x] = WallIcon;
                }
            }
        }

        private void WritePlayer()
        {
            var playerX = (int) (PlayerPosition.X + WallOffset);
            var playerY = (int) (PlayerPosition.Y + WallOffset);

            Buffer[playerY][playerX] = PlayerIcon;
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
                    "SankaraStone" => SankaraStoneIcon,
                    "Key" => KeyIcon,
                    "BoobyTrap" => BoobyTrapIcon,
                    "DisappearingBoobyTrap" => DisappearingBoobyTrapIcon,
                    "PressurePlate" => PressurePlateIcon,
                    _ => ItemIcon
                };

                Buffer[itemY][itemX] = icon;
            }
        }

        private void RenderDebug()
        {
            Buffer[24][0] = PlayerIcon;
            Buffer[24][1] = ':';
            
            var playerPosX = PlayerPosition.X.ToString().ToCharArray();
            
            for (var i = 0; i < playerPosX.Length; i++)
            {
                Buffer[24][i + 2] = playerPosX[i];
            }

            Buffer[23][0] = 'H';
            Buffer[23][1] = 'P';
            Buffer[23][2] = ':';
            
            var frameArr = PlayerHealth.ToString().ToCharArray();

            for (var i = 0; i < frameArr.Length; i++)
            {
                Buffer[23][i + 3] = frameArr[i];
            }
        }
    }
}