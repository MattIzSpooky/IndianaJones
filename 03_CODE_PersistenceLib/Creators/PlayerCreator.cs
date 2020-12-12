using System;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal class PlayerCreator : ICreator<Player>
    {
        private readonly int _scaleFactor;

        public PlayerCreator(int scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }

        public Player Create(JToken jsonToken)
        {
            var startX = jsonToken["startX"]?.Value<int>() + _scaleFactor;
            var startY = jsonToken["startY"]?.Value<int>() + _scaleFactor;
            var lives = jsonToken["lives"]?.Value<int>();

            if (startX == null || startY == null || lives == null)
                throw new NullReferenceException("Player JSON is invalid");

            return new Player(
                lives.Value,
                startX.Value,
                startY.Value
            );
        }
    }
}