using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    /// <summary>
    /// Create T based on given json object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ICreator<out T>
    {
        T Create(JToken jsonToken);
    }
}