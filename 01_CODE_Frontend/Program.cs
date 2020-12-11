using CODE_FileSystem;
using CODE_GameLib;
using System;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;
using MVC;

namespace CODE_Frontend
{
    public class Program
    {
        private Game _game;
        private Context _context;

        private Program()
        {
            var reader = new GameReader();
            _game = reader.Read(@"./Levels/TempleOfDoom.json");

            _context = new Context();
            _context.RegisterObject<Game>(_game);

            _context.OpenController<StartController, StartView>();
            
            _context.Run();
        }

        public static void Main(string[] args)
        {
            Console.Title = "Temple of DOOM"; // TODO: Maybe move to View?

            Console.WindowWidth = 100;
            Console.WindowHeight = 50;

            new Program();
        }
    }
}