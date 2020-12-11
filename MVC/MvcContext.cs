using System;
using System.Linq;
using System.Threading.Tasks;
using MVC.Views;
using MVC.Views.Console;

namespace MVC
{
    public class MvcContext : IDisposable
    {
        private IView _view;
        private bool _running = true;

        public void Run() => Task.Run(Update).Wait();

        private void Update()
        {
            Console.Clear(); // Remove warnings and stuff that just get in the way.

            _view.Draw();

            while (_running)
            {
                _view.KeyDown();
                _view.Draw();
            }
        }

        public void OpenController<TController, TConsoleView>(params object[] args)
            where TController : Controller<TConsoleView, ConsoleKey>
            where TConsoleView : ConsoleView
        {
            _view?.Dispose();
            
            var ctrArgs = new[] {this}.Concat(args).ToArray();
            
            var controller = (TController) Activator.CreateInstance(typeof(TController), ctrArgs);
            _view = controller?.View;
        }

        public void Stop()
        {
            _running = false;
        }

        public void Dispose() => _view.Dispose();
    }
}