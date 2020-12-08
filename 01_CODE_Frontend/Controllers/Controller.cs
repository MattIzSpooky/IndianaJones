using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public abstract class Controller
    {
        protected readonly Program _program;

        protected Controller(Program program)
        {
            _program = program;
        }

        public abstract void Update();
    }

    public abstract class Controller<T> : Controller where T : Controller<T>
    {
        protected Controller(Program program) : base(program)
        {
        }

        public abstract View<T> CreateView();
    }
}