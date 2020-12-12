using System;
using System.Linq;
using MVC.Views;

namespace MVC.Contexts
{
    public class MvcContext : IDisposable
    {
        private Controller _controller;
        private IView _view;
        private bool _running = true;

        public void Run() => Update();

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

        public void OpenController<TController, TConsoleView, TInput>(params object[] args)
            where TController : Controller<TConsoleView, TInput>
            where TConsoleView : View<TInput>
        {
            _controller?.Dispose();

            var ctrArgs = new[] {this}.Concat(args).ToArray();

            var controller = (TController) Activator.CreateInstance(typeof(TController), ctrArgs);
            _view = controller?.View;

            _controller = controller;
        }

        public void Stop() => _running = false;

        public void Dispose() => _controller.Dispose();
    }
}