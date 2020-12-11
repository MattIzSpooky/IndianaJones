using MVC.Views;

namespace MVC
{
    public abstract class Controller
    {
        protected readonly Context Root;

        protected Controller(Context root)
        {
            Root = root;
        }
    }

    public abstract class Controller<T, TInput> : Controller
        where T : View<TInput>
        where TInput : struct
    {
        public T View { protected set; get; }

        protected Controller(Context root) : base(root)
        {
        }

        protected abstract void SetUpView();
    }
}