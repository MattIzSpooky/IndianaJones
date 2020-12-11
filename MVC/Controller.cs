
using MVC.Views;

namespace MVC
{
    public abstract class Controller
    {
        protected readonly MvcContext Root;

        protected Controller(MvcContext root)
        {
            Root = root;
        }
    }

    public abstract class Controller<T, TInput> : Controller
        where T : View<TInput>
        where TInput : struct
    {
        public T View { protected set; get; }

        protected Controller(MvcContext root) : base(root)
        {
        }

        protected abstract void SetUpView();
    }
}