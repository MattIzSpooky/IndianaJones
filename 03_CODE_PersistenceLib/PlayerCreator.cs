using System;
using CODE_GameLib;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class PlayerCreator
    {
        private readonly int _scaleFactor;

        public PlayerCreator(int scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }

        public Player Create(JToken playerJson)
        {
            var startX = playerJson["startX"]?.Value<int>() + _scaleFactor;
            var startY = playerJson["startY"]?.Value<int>() + _scaleFactor;
            var lives = playerJson["lives"]?.Value<int>();

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