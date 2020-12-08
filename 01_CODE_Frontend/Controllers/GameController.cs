using System;
using CODE_Frontend.Views;
using CODE_GameLib;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameView>, IObserver<Game>
    {
        private Game _game;

        private string State { get; set; }
        
        public GameController(Program program, Game game) : base(program)
        {
            Initialize();
            
            _game = game;
            game.Register(this);
        }

        private void Initialize()
        {
            SetUpView();
        }

        protected override void SetUpView()
        {
            var view = new GameView();

            // Map movement
            view.MapInput(new Input(ConsoleKey.W, MoveUp));
            view.MapInput(new Input(ConsoleKey.A, MoveLeft));
            view.MapInput(new Input(ConsoleKey.S, MoveDown));
            view.MapInput(new Input(ConsoleKey.D, MoveRight));

            // Map other buttons.
            view.MapInput(new Input(ConsoleKey.Escape, QuitGame));

            View = view;
        }

        public void OnCompleted()
        {
        }

        private void MoveUp()
        {
            _game.MovePlayer(WindRose.North);
        }

        private void MoveDown()
        {
            _game.MovePlayer(WindRose.South);
        }

        private void MoveLeft()
        {
            _game.MovePlayer(WindRose.West);
        }

        private void MoveRight()
        {
            _game.MovePlayer(WindRose.East);
        }

        private void QuitGame()
        {
            Program.Stop();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Game value)
        {
            // TODO: Set game map on update.
            State = _game.Direction + Environment.NewLine + State;

            View.State = State;
        }


    }
}