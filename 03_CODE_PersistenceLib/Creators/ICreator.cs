using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal interface ICreator<out T>
    {
        T Create(JToken jsonToken);
    }
}