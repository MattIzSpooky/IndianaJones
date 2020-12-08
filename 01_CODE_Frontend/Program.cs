using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;

namespace CODE_Frontend
{
    public class Program
    {
        private bool _running = true;
        private Controller _controller;
        private View _view;

        private void OpenController<T>() where T : Controller<T>
        {
            var controller = (T) Activator.CreateInstance(typeof(T), this);
            
            _controller = controller;
            _view = controller?.CreateView();
        }
        
        private Program()
        {
            Initialize();

            Task.Run(Update).Wait();
        }

        private void Update()
        {
            while (_running)
            {
                // Game.Update();
                _controller.Update();

                var builder = new StringBuilder();

                _view.Draw(builder);

                // if (_queueClear)
                // {
                //     Console.Clear();
                //
                //     _queueClear = false;
                // }

                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                Console.Write(builder.ToString());

                Thread.Sleep(100);
            }
        }

        public static void Main(string[] args)
        {
            Console.Title = "Temple of DOOM";

            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            GameReader reader = new GameReader();
            Game game = reader.Read(@"./Levels/TempleOfDoom.json");

            // GameView gameView = new GameView();
            // game.Updated += (sender, game) => gameView.Draw(game);
            // game.Run();
            
            new Program();
        }
        public void Stop()
        {
            _running = false;
        }
        
        private void Initialize()
        {
            OpenController<GameController>();
        }
    }
}