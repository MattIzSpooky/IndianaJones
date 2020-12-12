using System;
using MVC.Contexts;
using MVC.Views;

namespace MVC
{
    public abstract class Controller : IDisposable
    {
        protected readonly MvcContext Root;

        protected Controller(MvcContext root)
        {
            Root = root;
        }

        public abstract void Dispose();
    }

    public abstract class Controller<T, TInput> : Controller
        where T : View<TInput>
    {
        public T View { protected set; get; }

        protected Controller(MvcContext root) : base(root)
        {
        }

        protected abstract void SetUpView();
        public override void Dispose() => View.Dispose();
    }
}