using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CODE_Frontend.Views;
using CODE_GameLib;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameView>, IObserver<Game>
    {
        private Game _game;

        public GameController(Program program, Game game) : base(program)
        {
            _game = game;
            game.Register(this);
            
            Initialize();
        }

        private void Initialize()
        {
            SetUpView();
            OnNext(_game);
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
            View.RoomHeight = _game.CurrentRoom.Height;
            View.RoomWidth = _game.CurrentRoom.Width;
            
            View.PlayerPosition = new Vector2(_game.Player.X, _game.Player.Y);
            View.PlayerHealth = _game.Player.Lives;
            
            View.Items = _game.CurrentRoom.InteractableTiles.Select(i => new ViewableItem()
            {
                Position = new Vector2(i.X, i.Y),
                Type = i.GetType() // TODO: eww
            }).ToArray();
        }
    }
}