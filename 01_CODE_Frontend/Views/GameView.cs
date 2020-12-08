using System.Text;
using CODE_Frontend.Controllers;

namespace CODE_Frontend.Views
{
    public class GameView : View<GameController>
    {
        public GameView(GameController controller) : base(controller, CreateInputs(controller))
        {
        }

        public override void Draw(StringBuilder builder)
        {
            builder.Append(_controller.State);
        }

        private static Input[] CreateInputs(GameController controller)
        {
            return new[]
            {
                new Input('w', controller.MoveUp),
                new Input('a', controller.MoveLeft),
                new Input('s', controller.MoveDown),
                new Input('d', controller.MoveRight)
            };
        }
    }
}