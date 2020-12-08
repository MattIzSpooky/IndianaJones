using System.Text;
using CODE_Frontend.Controllers;

namespace CODE_Frontend.Views
{
    public abstract class View
    {
        private readonly char[] _tiles;

        internal View(char[] tiles)
        {
            _tiles = tiles;
        }

        public abstract void Draw(StringBuilder builder);
    }

    public abstract class View<T> : View where T : Controller<T>
    {
        protected readonly T _controller;

        protected View(T controller, params char[] tiles) : base(tiles)
        {
            _controller = controller;
        }
    }
}