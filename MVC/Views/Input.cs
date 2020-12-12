using System;

namespace MVC.Views
{
    public class Input<T>
    {
        public T Key { get; }
        public Action Action { get; }

        public Input(T key, Action action)
        {
            Key = key;
            Action = action;
        }
    }
}