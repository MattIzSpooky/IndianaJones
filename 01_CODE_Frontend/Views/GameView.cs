using System.Text;

namespace CODE_Frontend.Views
{
    public class GameView : View
    {
        public string State { private get; set; }

        public override void Draw(StringBuilder builder)
        {
            builder.Append(State);
        }
    }
}