using System;
using CODE_Frontend.Views;
using MVC;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class StartController : Controller<StartView, ConsoleKey>
    {
        public StartController(MvcContext root) : base(root)
        {
            Initialize();
        }

        private void Initialize()
        {
            SetUpView();
        }

        protected override void SetUpView()
        {
            var view = new StartView();

            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Start));

            View = view;
        }

        private void Start() => Root.OpenController<GameController, GameView>();
    }
}