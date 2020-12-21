using System;
using System.Linq;
using System.Numerics;
using CODE_Frontend.Mappers;
using CODE_Frontend.ViewModels;
using CODE_Frontend.Views;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using CODE_PersistenceLib;
using MVC;
using MVC.Contexts;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameView, ConsoleKey>, IObserver<Game>
    {
        private Game _game;

        private readonly IMapper<IInteractable, InteractableViewModel> _interactableMapper =
            new InteractableMapper();

        private readonly HallwayMapper _hallwayMapper = new HallwayMapper();

        public GameController(MvcContext root) : base(root)
        {
            var reader = new GameReader();
            _game = reader.Read(@"./Levels/TempleOfDoom.json");

            _game.Register(this);

            Initialize();
        }

        private void Initialize()
        {
            SetUpView();
        }

        protected override void SetUpView()
        {
            var view = new GameView();

            // Map movement
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.W, MoveUp));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.A, MoveLeft));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.S, MoveDown));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D, MoveRight));

            // Map other buttons.
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, QuitGame));

            View = view;

            UpdateViewModels();
        }

        public void OnCompleted()
        {
        }

        private void MoveUp() => _game.MovePlayer(Direction.North);
        private void MoveDown() => _game.MovePlayer(Direction.South);

        private void MoveLeft() => _game.MovePlayer(Direction.West);

        private void MoveRight() => _game.MovePlayer(Direction.East);

        private void QuitGame() => Root.OpenController<EndController, EndView, ConsoleKey>(_game);

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Game value)
        {
            if (_game.HasEnded)
            {
                QuitGame();
                return;
            }

            UpdateViewModels();
        }

        private void UpdateViewModels()
        {
            View.Room = new RoomViewModel
            {
                Id = _game.CurrentRoom.Id,
                Height = _game.CurrentRoom.Height,
                Width = _game.CurrentRoom.Width
            };

            View.Player = new PlayerViewModel
            {
                Lives = _game.Player.Lives,
                Position = new Vector2(_game.Player.X, _game.Player.Y),
                Score = _game.Player.Score
            };

            View.Interactables = _game.CurrentRoom.Interactables.Select(_interactableMapper.MapTo).ToArray();

            _hallwayMapper.RoomId = _game.CurrentRoom.Id;
            View.Hallways = _game.CurrentRoom.Hallways.Select(_hallwayMapper.MapTo).ToArray();
        }

        ~GameController()
        {
            _game.Unregister(this);
        }
    }
}