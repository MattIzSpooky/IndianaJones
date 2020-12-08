using System;
using System.Text;
using CODE_Frontend.Controllers;

namespace CODE_Frontend.Views
{
    public class GameView : View<GameController>
    {
        public GameView(GameController controller, params char[] tiles) : base(controller, tiles)
        {
        }

        public override void Draw(StringBuilder builder)
        {
            Console.Write(builder.ToString());
        }
    }
}