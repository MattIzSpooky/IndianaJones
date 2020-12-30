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
        private readonly Game _game;

        private readonly IMapper<IInteractable, InteractableViewModel> _interactableMapper = new InteractableMapper();
        private readonly IMapper<Cheat, ViewableCheat> _cheatMapper = new CheatMapper();

        public GameController(MvcContext root) : base(root)
        {
            var reader = new GameReader();
            _game = reader.Read(@"./Levels/TempleOfDoom_Extended_A.json");

            _game.Register(this);
        }

        public override void SetUpView()
        {
            var view = new GameView();

            // Map movement
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, MoveUp));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.LeftArrow, MoveLeft));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, MoveDown));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.RightArrow, MoveRight));

            // Map attack
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Attack));
            
            // Map cheats
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D, ToggleWalkThroughDoors));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.L, ToggleInvincibilityCheat));

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

        private void ToggleInvincibilityCheat() => _game.ToggleCheat(Cheat.Invincible);
        private void ToggleWalkThroughDoors() => _game.ToggleCheat(Cheat.MoveThroughDoors);
        private void Attack() => _game.PlayerAttack();

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
                Lives = _game.Player.NumberOfLives,
                Position = new Vector2(_game.Player.X, _game.Player.Y),
                Score = _game.Player.Score
            };

            View.Interactables = _game.CurrentRoom.Interactables.Select(_interactableMapper.MapTo).ToArray();

            // Only grab the enabled cheats.
            View.EnabledCheats = _game.Cheats
                .Where(pair => pair.Value)
                .Select(pair => _cheatMapper.MapTo(pair.Key)).ToArray();
        }

        ~GameController()
        {
            _game.Unregister(this);
        }
    }
}