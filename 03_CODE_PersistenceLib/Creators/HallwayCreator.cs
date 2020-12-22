using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Connections;
using CODE_GameLib.Doors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal class HallwayCreator : ICreator<Hallway>
    {
        private readonly DoorFactory _doorFactory = new DoorFactory();
        private readonly IEnumerable<Room> _rooms;

        public HallwayCreator(IEnumerable<Room> rooms)
        {
            _rooms = rooms;
        }

        public Hallway Create(JToken jsonToken)
        {
            IDoor door = null;
            var directions = new Dictionary<Direction, Room>();
            
            foreach (var child in jsonToken.Children().OfType<JProperty>())
            {
                if (child.Name.Equals("door"))
                {
                    var type = (child.First?["type"] ?? "").Value<string>();
                    var color = (child.First?["color"] ?? "").Value<string>();

                    door = _doorFactory.Create(type, color);
                }
                else
                {
                    var roomId = child.Value.ToObject<int>();
                    directions.Add(Enum.Parse<Direction>(child.Name, true),
                        _rooms.FirstOrDefault(r => r.Id == roomId));
                }
            }

            return new Hallway(directions, door!); // Suppress warning cuz door can be null.
        }

        public IEnumerable<Hallway> CreateMultiple(IEnumerable<JToken> jsonToken) => jsonToken.Select(Create).ToList();
    }
}