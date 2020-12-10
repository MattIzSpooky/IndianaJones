using System.Collections.Generic;

namespace MVC.Views
{
    public abstract class View<T> : IView where T : struct
    {
        public int Height { get; }
        public int Width { get; }
        
        protected readonly List<Input<T>> Inputs = new List<Input<T>>();

        public View(int width, int height)
        {
            Height = height;
            Width = width;
        }
        
        public void MapInput(Input<T> consoleInput)
        {
            Inputs.Add(consoleInput);
        }
        
        public abstract void Dispose();
        public abstract void Draw();
        public abstract void KeyDown();
    }
}