using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public abstract class Controller
    {
        protected readonly Program Program;

        protected Controller(Program program)
        {
            Program = program;
        }
    }

    public abstract class Controller<T> : Controller where T : View
    {
        public T View
        {
            protected set;
            get;
        }
        protected Controller(Program program) : base(program)
        { }
        
        protected abstract void SetUpView();
    }
}