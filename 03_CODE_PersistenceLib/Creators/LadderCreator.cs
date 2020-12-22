using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    public class LadderCreator : ICreator<object>
    {
        public object Create(JToken jsonToken)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<object> CreateMultiple(IEnumerable<JToken> jsonTokens)
        {
            throw new System.NotImplementedException();
        }
    }
}