using System;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;
using CODE_PersistenceLib;
using MVC.Contexts;

namespace CODE_Frontend
{
    public class Program
    {
        private Program()
        {
            using var mvcContext = new MvcContext();
            mvcContext.OpenController<StartController, StartView, ConsoleKey>();
            mvcContext.Run();
        }

        public static void Main(string[] args)
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 50;

            new Program();
        }
    }
}