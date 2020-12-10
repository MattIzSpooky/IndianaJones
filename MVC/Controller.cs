using MVC.Views;

namespace MVC
{
    public abstract class Controller<TRoot>
    {
        protected readonly TRoot Root;
        
        protected Controller(TRoot root)
        {
            Root = root;
        }
    }

    public abstract class Controller<T, TRoot> : Controller<TRoot> where T : View
    {
        public T View
        {
            protected set;
            get;
        }
        protected Controller(TRoot root) : base(root)
        { }
        
        protected abstract void SetUpView();
    }
}