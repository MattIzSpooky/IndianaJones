using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        private readonly InteractableTileFactory _interactableTileFactory = new InteractableTileFactory(); // TODO: Use DI.

        public Game Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) 
                throw new ArgumentException("filePath was null or empty.");

            var json = JObject.Parse(File.ReadAllText(filePath));

            var playerJson = json["player"];

            if (playerJson == null) 
                throw new NullReferenceException("No player JSON was found.");

            var player = CreatePlayer(playerJson);
            var startRoomId = playerJson["startRoomId"]; // TODO: Throw exception if null.

            var rooms = CreateRooms(json["rooms"]);
            var connections = CreateConnections(json["connections"],rooms.ToList());
            
            // TODO: Combine connections to Rooms
            // TODO: Put player in start room.
            
            // TODO: Put everything on game.

            return new Game();
        }

        private IEnumerable<Connection> CreateConnections(JToken connectionsJson, List<Room> rooms)
        {
            // TODO: Create Connections
            
            if (!connectionsJson.HasValues) 
                throw new ArgumentException("Connections JSON is invalid.");

            for (var i = 0; i < connectionsJson.Count(); i++)
            {
                if(i == 0)
                    continue;
                
                var connection = (JObject) connectionsJson[i];
                
                /*
                 * This crashes because the door should also be deserialized. 
                 */
                foreach (var jProperty in connection.Children().OfType<JProperty>())
                {
                    var roomId = jProperty.Value.ToObject<int>();
                    var nextRoom = rooms.Find(c => c.Id == roomId);
                    var windRose = Enum.Parse<WindRose>(jProperty.Name,true);
                    
                    //room.SetConnections(new Connection(nextRoom,windRose));
                    
                    Console.WriteLine($"Key: {windRose}, Value: {nextRoom.Id}");
                }
            }

            return null;
        }
        
        private IEnumerable<Room> CreateRooms(JToken roomsJson)
        {
            if (!roomsJson.HasValues) 
                throw new ArgumentException("Rooms JSON is invalid.");
            
            var rooms = new List<Room>();

            foreach (var roomJson in roomsJson)
            {
                var id = roomJson["id"]?.Value<int>();
                var width = roomJson["width"]?.Value<int>();
                var height = roomJson["height"]?.Value<int>();
                var type = roomJson["type"]?.Value<string>();

                if (id == null || width == null || height == null || type == null)
                    throw new NullReferenceException("Rooms JSON is invalid");

                if (type != "room") 
                    throw new Exception($"Room {id.Value} does not have required type 'room'");

                var itemsJson = roomJson["items"];

                if (itemsJson != null)
                {
                    var room = new Room(
                        id.Value,
                        width.Value,
                        height.Value
                    );

                    itemsJson
                        .Select(json => CreateInteractableTile(json, room))
                        .ToList()
                        .ForEach(i => room.AddInteractableTile(i));

                    rooms.Add(room);
                }
                else
                {
                    rooms.Add(new Room(
                        id.Value,
                        width.Value,
                        height.Value
                    ));
                }
            }

            return rooms;
        }

        private InteractableTile CreateInteractableTile(JToken tileJson, Room room)
        {
            var type = tileJson["type"]?.Value<string>();
            var x = tileJson["x"]?.Value<int>();
            var y = tileJson["y"]?.Value<int>();

            if (type == null || x == null || y == null) throw new NullReferenceException("Item JSON is invalid.");

            // Optionals.
            var damage = tileJson["damage"]?.Value<string>(); // Is int in actual JSON. might cause problems.
            var color = tileJson["color"]?.Value<string>();

            var args = damage ?? color;

            return _interactableTileFactory.Create(
                type,
                room,
                x.Value,
                y.Value,
                args
            );
        }

        private Player CreatePlayer(JToken playerJson)
        {
            var startX = playerJson["startX"]?.Value<int>();
            var startY = playerJson["startY"]?.Value<int>();
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