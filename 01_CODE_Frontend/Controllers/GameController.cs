using System;
using CODE_Frontend.Views;
using CODE_GameLib;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameController>, IObserver<Game>
    {
        private Game _game;
        
        // TODO: Expose the game map.
        
        public string State { get; private set; }
        public GameController(Program program, Game game) : base(program)
        {
            _game = game;
            game.Register(this);
        }

        public override View<GameController> CreateView()
        {
            return new GameView(this);
        }

        public void OnCompleted()
        {
            
        }

        public void MoveUp()
        {
            _game.MovePlayer(WindRose.North);
        }
        public void MoveDown()
        {
            _game.MovePlayer(WindRose.South);
        }
        public void MoveLeft()
        {
            _game.MovePlayer(WindRose.West);
        }
        public void MoveRight()
        {
            _game.MovePlayer(WindRose.East);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Game value)
        {
            // TODO: Set game map on update.
            State = _game.Direction.ToString();
        }
    }
}