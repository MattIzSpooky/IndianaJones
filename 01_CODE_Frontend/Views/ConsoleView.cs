using Colorful;

namespace CODE_Frontend.Views
{
    public abstract class ConsoleView : View
    {
        public int Height { get; }
        public int Width { get; }

        protected readonly char[][] Buffer;

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
                    Buffer[y][x] = ' ';
                }
            }
        }

        protected virtual void WriteBuffer()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            for (var y = 0; y < Height; ++y) Console.WriteLine(Buffer[y]);
        }

        private char[][] CreateBuffer()
        {
            var render = new char[Height][];

            for (var y = 0; y < Width; ++y)
                render[y] = new char[Width];

            return render;
        }

        public override void Dispose()
        {
            Console.Clear();
            Console.ResetColor();
        }
    }
}