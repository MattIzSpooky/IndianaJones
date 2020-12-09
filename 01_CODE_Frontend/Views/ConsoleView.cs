using Colorful;

namespace CODE_Frontend.Views
{
    public abstract class ConsoleView : View
    {
        public override void Dispose()
        {
            Console.Clear();
            Console.ResetColor();
        }
    }
}