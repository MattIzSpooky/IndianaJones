using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using CODE_Frontend.Views;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Doors;
using CODE_GameLib.Interactable.Trap;
using MVC;
using MVC.Views;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameView, ConsoleKey, Program>, IObserver<Game>
    {
        private Game _game;

        public GameController(Program root, Game game) : base(root)
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
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.W, MoveUp));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.A, MoveLeft));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.S, MoveDown));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D, MoveRight));

            // Map other buttons.
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, QuitGame));

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

        private void QuitGame() => Root.OpenController<EndController, EndView>();

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

            View.RoomHeight = _game.CurrentRoom.Height;
            View.RoomWidth = _game.CurrentRoom.Width;

            View.PlayerPosition = new Vector2(_game.Player.X, _game.Player.Y);
            View.PlayerHealth = _game.Player.Lives;

            View.Interactables = _game.CurrentRoom.InteractableTiles.Select(GetViewableInteractable).ToArray();

            View.Doors = _game.CurrentRoom.Hallways.Select(GetViewableDoor).ToArray();
        }

        private ViewableDoor GetViewableDoor(Hallway hallway)
        {
            var direction = hallway.GetDirectionByRoom(_game.CurrentRoom.Id);

            if (hallway.DoorContext != null)
            {
                return hallway.DoorContext.Door switch
                {
                    ClosingGate _ => new ViewableDoor {Character = '∩', Color = Color.White, Direction = direction},
                    ColoredDoor coloredDoor => new ViewableDoor
                        {Character = '=', Color = coloredDoor.Color, Direction = direction},
                    ToggleDoor _ => new ViewableDoor {Character = '┴', Color = Color.White, Direction = direction},
                };
            }

            return new ViewableDoor {Character = ' ', Color = Color.Empty, Direction = direction,};
        }

        private static ViewableInteractable GetViewableInteractable(InteractableTile interactableTile)
        {
            var interactable = interactableTile switch
            {
                Key key => new ViewableInteractable {Character = 'K', Color = key.Color,},
                SankaraStone _ => new ViewableInteractable {Character = 'S', Color = Color.Orange},
                DisappearingBoobyTrap _ => new ViewableInteractable {Character = '@', Color = Color.White},
                PressurePlate _ => new ViewableInteractable {Character = 'T', Color = Color.White},
                BoobyTrap _ => new ViewableInteractable {Character = 'O', Color = Color.White},
                Wall _ => new ViewableInteractable {Character = '#', Color = Color.Yellow},
                _ => new ViewableInteractable {Character = 'Z', Color = Color.White}
            };

            interactable.Position = new Vector2(interactableTile.X, interactableTile.Y);

            return interactable;
        }
    }
}