using System;

namespace MVC.Views
{
    public interface IView : IDisposable
    {
        public void Draw();
        public void KeyDown();
    }
}