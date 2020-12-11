﻿namespace CODE_GameLib.Interactable.Doors
{
    public class ClosingGate : IDoor
    {
        private bool _isOpen = true;
        
        public bool Open(Player player)
        {
            if (!_isOpen) return _isOpen;
            
            _isOpen = false;
            
            return true;
        }
    }
}