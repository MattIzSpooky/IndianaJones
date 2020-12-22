using System;
using System.Collections.Generic;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal class PlayerCreator : ICreator<Player>
    {
        public Player Create(JToken jsonToken)
        {
            var startX = jsonToken["startX"]?.Value<int>();
            var startY = jsonToken["startY"]?.Value<int>();
            var lives = jsonToken["lives"]?.Value<int>();

            if (startX == null || startY == null || lives == null)
                throw new NullReferenceException("Player JSON is invalid");

            return new Player(
                lives.Value,
                startX.Value,
                startY.Value
            );
        }

        public IEnumerable<Player> CreateMultiple(IEnumerable<JToken> jsonTokens)
        {
            throw new NotImplementedException();
        }
    }
}