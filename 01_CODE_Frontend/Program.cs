using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;
using System.Threading.Tasks;
using CODE_Frontend.Controllers;
using MVC.Views;

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
            Console.Title = "Temple of DOOM";

            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 50;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            new Program();
        }

        public void Stop()
        {
            _running = false;
        }

        private void Initialize()
        {
            var reader = new GameReader();
            _game = reader.Read(@"./Levels/TempleOfDoom.json");

            var controller = new GameController(this, _game);
            _view = controller.View;

            Console.Clear(); // Remove warnings and stuff that just get in the way.
        }
    }
}