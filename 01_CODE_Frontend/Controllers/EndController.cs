using System;
using CODE_Frontend.Views;
using CODE_GameLib;
using MVC;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class EndController : Controller<EndView, ConsoleKey, Program>
    {
        private Game _game;

        public EndController(Program root, Game game) : base(root)
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

            view.PlayerHealth = _game.Player.Lives;
            view.StonesCollected = _game.Player.Score;

            View = view;
        }

        private void End() => Root.Stop();
    }
}