using System;
using System.Text;
using CODE_Frontend.Controllers;

namespace CODE_Frontend.Views
{
    public abstract class View
    {
        private Input[] _inputs;

        internal View()
        {
        }
        
        internal View(Input[] inputs)
        {
            _inputs = inputs;
        }

        public abstract void Draw(StringBuilder builder);

        public void HandleInput()
        {
            var key = Console.ReadKey(true);

            foreach (var input in _inputs)
            {
                if (input.Character != key.KeyChar)
                {
                    continue;
                }

                input.Action();
            }
        }
    }

    public abstract class View<T> : View where T : Controller<T>
    {
        protected readonly T _controller;

        protected View(T controller, params Input[] inputs) : base(inputs)
        {
            _controller = controller;
        }
    }
}