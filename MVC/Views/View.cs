using System;
using System.Collections.Generic;
using System.Linq;
using MVC.Views.Console;

namespace MVC.Views
{
    public abstract class View
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
            var key = System.Console.ReadKey(true).Key;

            foreach (var input in _inputs.Where(input => input.Key == key))
            {
                input.Action();
            }
        }
    }
}