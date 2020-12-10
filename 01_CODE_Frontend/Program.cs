using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Threading.Tasks;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;
using MVC;
using MVC.Views;
using MVC.Views.Console;

namespace CODE_Frontend
{
    public class Program
    {
        private IView _view;
        private Game _game;
        private bool _running = true;

        private Program()
        {
            Initialize();

            Task.Run(Update).Wait();

            _view.Dispose();
        }

        private void Update()
        {
            _view.Draw();

            while (_running)
            {
                _view.KeyDown();
                _view.Draw();
            }
        }

        public static void Main(string[] args)
        {
            Console.Title = "Temple of DOOM"; // TODO: Maybe move to View?

            Console.WindowWidth = 100;
            Console.WindowHeight = 50;
            
            new Program();
        }

        public void Stop()
        {
            _running = false;
        }

        public void OpenController<TController, TConsoleView>()
            where TController : Controller<TConsoleView, ConsoleKey, Program>
            where TConsoleView : ConsoleView
        {
            var controller = (TController) Activator.CreateInstance(typeof(TController), this, _game);
            _view = controller?.View;
        }

        private void Initialize()
        {
            var reader = new GameReader();
            _game = reader.Read(@"./Levels/TempleOfDoom.json");

            OpenController<GameController, GameView>();

            Console.Clear(); // Remove warnings and stuff that just get in the way.
        }
    }
}