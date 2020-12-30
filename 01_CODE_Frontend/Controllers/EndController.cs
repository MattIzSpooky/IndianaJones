using System;
using CODE_Frontend.ViewModels;
using CODE_Frontend.Views;
using CODE_GameLib;
using MVC;
using MVC.Contexts;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class EndController : Controller<EndView, ConsoleKey>
    {
        private readonly Game _game;

        public EndController(MvcContext root, Game game) : base(root)
        {
            _game = game;
        }

        public override void SetUpView()
        {
            var view = new EndView();

            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, End));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, End));
            
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.R, Restart));

            view.PlayerViewModel = new PlayerViewModel
            {
                Lives = _game.Player.NumberOfLives,
                Score = _game.Player.Score
            };
            view.Stones = _game.Stones;

            View = view;
        }

        private void End() => Root.Stop();
        private void Restart() => Root.OpenController<StartController, StartView, ConsoleKey>();
    }
}