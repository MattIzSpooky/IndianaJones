﻿using System;
using System.Drawing;
using System.Linq;
using System.Text;
using SysConsole = Colorful.Console;

namespace MVC.Views.Console
{
    public abstract class ConsoleView : View<ConsoleKey>
    {
        protected readonly ColoredChar[][] Buffer;
        protected int StringCursor;

        protected ConsoleView(int width, int height, string title) : base(width, height, title)
        {
            Buffer = CreateBuffer();

            SysConsole.OutputEncoding = Encoding.UTF8;
            SysConsole.Title = title;
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
            SysConsole.SetCursorPosition(0, 0);
            SysConsole.CursorVisible = false;

            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Buffer[y].Length; x++)
                {
                    var coloredChar = Buffer[y][x];

                    if (coloredChar.Color.IsEmpty) SysConsole.Write(coloredChar.Character);
                    else SysConsole.Write(coloredChar.Character, coloredChar.Color);
                }

                SysConsole.WriteLine();
            }
        }

        protected void WriteString(string text, Color color)
        {
            var chars = text.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                Buffer[StringCursor][i] = CreateChar(chars[i], color);
            }
            
            StringCursor++;
        }

        private ColoredChar[][] CreateBuffer()
        {
            var render = new ColoredChar[Height][];

            for (var y = 0; y < Width; ++y)
                render[y] = new ColoredChar[Width];

            return render;
        }

        public override void KeyDown()
        {
            var key = SysConsole.ReadKey(true).Key;

            foreach (var input in Inputs.Where(input => input.Key == key))
            {
                input.Action();
            }
        }

        public override void Dispose()
        {
            SysConsole.ResetColor();
            SysConsole.Clear();

            GC.SuppressFinalize(this);
        }
    }
}