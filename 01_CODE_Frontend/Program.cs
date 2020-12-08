using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;
using System.Threading.Tasks;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;

namespace CODE_Frontend
{
    public class Program
    {
        private View _view;
        private Game _game;
        private bool _running = true;
        
        private Program()
        {
            Initialize();

            Task.Run(Update);
            Task.Run(Input).Wait();
        }

        private void Input()
        {
            while (_running)
            {
                _view.KeyDown();
            }
        }

        private void Update()
        {
            while (_running)
            {
                var builder = new StringBuilder();

                _view.Draw(builder);

                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                Console.Write(builder.ToString());
            }
        }

        public static void Main(string[] args)
        {
            Console.Title = "Temple of DOOM";

            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
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