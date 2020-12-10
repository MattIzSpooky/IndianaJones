using System;

namespace MVC.Views.Console
{
    public class Input
    {
        public ConsoleKey Key { get; }
        public Action Action { get; }

        public Input(ConsoleKey key, Action action)
        {
            Key = key;
            Action = action;
        }
    }
}