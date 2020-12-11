using System;
using CODE_Frontend.Views;
using CODE_GameLib;
using MVC;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class EndController : Controller<EndView, ConsoleKey>
    {
        private Game _game;

        public EndController(MvcContext root, Game game) : base(root)
        {
            _game = game;

            Initialize();
        }

        private void Initialize()
        {
            SetUpView();
        }

        protected override void SetUpView()
        {
            var view = new EndView();

            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, End));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, End));
            
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.R, Restart));

            view.PlayerHealth = _game.Player.Lives;
            view.StonesCollected = _game.Player.Score;

            View = view;
        }

        private void End() => Root.Stop();
        private void Restart() => Root.OpenController<StartController, StartView>();
    }
}