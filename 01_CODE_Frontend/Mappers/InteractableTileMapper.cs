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
    public class InteractableTileMapper : IMapper<InteractableTile, InteractableViewModel>
    {
        private const char Key = 'K';
        private const char SankaraStone = 'S';
        private const char DisappearingBoobyTrap = '@';
        private const char PressurePlate = 'T';
        private const char BoobyTrap = 'O';
        private const char Wall = '#';
        
        private const char Default = '?';
        
        
        public InteractableViewModel MapTo(InteractableTile from)
        {
            var interactable = from switch
            {
                Key key => new InteractableViewModel {Character = Key, Color = key.Color},
                SankaraStone _ => new InteractableViewModel {Character = SankaraStone, Color = Color.Orange},
                DisappearingBoobyTrap _ => new InteractableViewModel {Character = DisappearingBoobyTrap, Color = Color.White},
                PressurePlate _ => new InteractableViewModel {Character = PressurePlate, Color = Color.White},
                BoobyTrap _ => new InteractableViewModel {Character = BoobyTrap, Color = Color.White},
                Wall _ => new InteractableViewModel {Character = Wall, Color = Color.Yellow},
                _ => new InteractableViewModel {Character = Default, Color = Color.White}
            };

            interactable.Position = new Vector2(from.X, from.Y);

            return interactable;
        }
    }
}