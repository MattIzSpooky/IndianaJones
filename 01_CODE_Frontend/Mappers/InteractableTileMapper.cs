using System.Drawing;
using System.Numerics;
using CODE_Frontend.Views;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Trap;

namespace CODE_Frontend.Mappers
{
    public class InteractableTileMapper : IMapper<InteractableTile, ViewableInteractable>
    {
        public ViewableInteractable MapTo(InteractableTile from)
        {
            var interactable = from switch
            {
                Key key => new ViewableInteractable {Character = 'K', Color = key.Color,},
                SankaraStone _ => new ViewableInteractable {Character = 'S', Color = Color.Orange},
                DisappearingBoobyTrap _ => new ViewableInteractable {Character = '@', Color = Color.White},
                PressurePlate _ => new ViewableInteractable {Character = 'T', Color = Color.White},
                BoobyTrap _ => new ViewableInteractable {Character = 'O', Color = Color.White},
                Wall _ => new ViewableInteractable {Character = '#', Color = Color.Yellow},
                _ => new ViewableInteractable {Character = 'Z', Color = Color.White}
            };

            interactable.Position = new Vector2(from.X, from.Y);

            return interactable;
        }
    }
}