using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Views;
using MVC.Views.Console;

namespace MVC
{
    public class Context
    {
        private IView _view;
        private bool _running = true;

        private readonly List<Object> _objects = new List<Object>();

        public void Run()
        {
            Task.Run(Update).Wait();
            _view.Dispose();
        }
        
        public void Update()
        {
            Console.Clear(); // Remove warnings and stuff that just get in the way.

            _view?.Draw();

            while (_running)
            {
                _view?.KeyDown();
                _view?.Draw();
            }
        }

        public void RegisterObject<T>(T obj) => _objects.Add(obj);
        
        public void OpenController<TController, TConsoleView>()
            where TController : Controller<TConsoleView, ConsoleKey>
            where TConsoleView : ConsoleView
        {
            var controller = (TController) Activator.CreateInstance(typeof(TController), this);
            _view = controller?.View;
        }
        
        public void Stop()
        {
            _running = false;
        }
    }
}