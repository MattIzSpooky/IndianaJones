using System;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;
using MVC;

namespace CODE_Frontend
{
    public class Program
    {
        private Program()
        {
            using var mvcContext = new MvcContext();
            
            mvcContext.OpenController<StartController, StartView>();
            
            mvcContext.Run();
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