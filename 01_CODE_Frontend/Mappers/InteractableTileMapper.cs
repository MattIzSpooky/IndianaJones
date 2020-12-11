using System.Drawing;
using System.Numerics;
using CODE_Frontend.ViewModels;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Collectable;
using CODE_GameLib.Interactable.Trap;
using MVC;

namespace CODE_Frontend.Mappers
{
    public class InteractableTileMapper : IMapper<InteractableTile, ViewableInteractable>
    {
        private const char Key = 'K';
        private const char SankaraStone = 'S';
        private const char DisappearingBoobyTrap = '@';
        private const char PressurePlate = 'T';
        private const char BoobyTrap = 'O';
        private const char Wall = '#';
        
        private const char Default = '?';
        
        
        public ViewableInteractable MapTo(InteractableTile from)
        {
            var interactable = from switch
            {
                Key key => new ViewableInteractable {Character = Key, Color = key.Color,},
                SankaraStone _ => new ViewableInteractable {Character = SankaraStone, Color = Color.Orange},
                DisappearingBoobyTrap _ => new ViewableInteractable {Character = DisappearingBoobyTrap, Color = Color.White},
                PressurePlate _ => new ViewableInteractable {Character = PressurePlate, Color = Color.White},
                BoobyTrap _ => new ViewableInteractable {Character = BoobyTrap, Color = Color.White},
                Wall _ => new ViewableInteractable {Character = Wall, Color = Color.Yellow},
                _ => new ViewableInteractable {Character = Default, Color = Color.White}
            };

            interactable.Position = new Vector2(from.X, from.Y);

            return interactable;
        }
    }
}