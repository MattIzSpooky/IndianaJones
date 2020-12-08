using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameController>
    {
        public GameController(Program program) : base(program)
        {
        }

        public override View<GameController> CreateView()
        {
            return new GameView(this, 't');
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}