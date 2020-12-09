using System;
using System.Collections.Generic;
using System.Linq;

namespace CODE_Frontend.Views
{
    public abstract class View : IDisposable
    {
        private readonly List<Input> _inputs = new List<Input>();

        public void MapInput(Input input)
        {
            _inputs.Add(input);
        }
        
        public abstract void Draw();
        public abstract void Dispose();

        public void KeyDown()
        {
            var key = Console.ReadKey(true).Key;

            foreach (var input in _inputs.Where(input => input.Key == key))
            {
                input.Action();
            }
        }
    }
}