using System.Collections.Generic;

namespace MVC.Views
{
    public abstract class View<T> : IView where T : struct
    {
        protected readonly List<Input<T>> Inputs = new List<Input<T>>();

        public void MapInput(Input<T> consoleInput)
        {
            Inputs.Add(consoleInput);
        }
        
        public abstract void Dispose();
        public abstract void Draw();
        public abstract void KeyDown();
    }
}