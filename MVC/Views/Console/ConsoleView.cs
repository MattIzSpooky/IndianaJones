using System.Drawing;
using static System.GC;

namespace MVC.Views.Console
{
    public abstract class ConsoleView : View
    {
        public int Height { get; }
        public int Width { get; }

        protected readonly ColoredChar[][] Buffer;

        protected ConsoleView(int width, int height)
        {
            Height = height;
            Width = width;

            Buffer = CreateBuffer();
        }

        protected void ClearBuffer()
        {
            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Width; ++x)
                {
                    Buffer[y][x] = new ColoredChar
                    {
                        Character = ' '
                    };
                }
            }
        }

        protected ColoredChar CreateChar(char character)
        {
            return CreateChar(character, Color.White);
        }

        protected ColoredChar CreateChar(char character, Color color)
        {
            return new ColoredChar {Character = character, Color = color};
        }

        protected void WriteBuffer()
        {
            Colorful.Console.SetCursorPosition(0, 0);
            Colorful.Console.CursorVisible = false;

            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Buffer[y].Length; x++)
                {
                    var coloredChar = Buffer[y][x];

                    if (coloredChar.Color.IsEmpty) Colorful.Console.Write(coloredChar.Character);
                    else Colorful.Console.Write(coloredChar.Character, coloredChar.Color);
                }

                Colorful.Console.WriteLine();
            }
        }

        private ColoredChar[][] CreateBuffer()
        {
            var render = new ColoredChar[Height][];

            for (var y = 0; y < Width; ++y)
                render[y] = new ColoredChar[Width];

            return render;
        }
        
        public override void Dispose()
        {
            Colorful.Console.ResetColor();
            Colorful.Console.Clear();

            SuppressFinalize(this);
        }
    }
}