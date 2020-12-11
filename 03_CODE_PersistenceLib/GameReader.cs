using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Doors;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        private readonly InteractableTileFactory
            _interactableTileFactory = new InteractableTileFactory(); // TODO: Use DI.

        private readonly DoorFactory _doorFactory = new DoorFactory(); // TODO: Use DI.
        
        private const byte ScaleFactor = 1;

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

            foreach (var room in rooms)
            {
                SetWalls(room);
            }

            rooms.FirstOrDefault(r => r.Id == startRoomId.Value<int>()).Player = player;

            SetConnectionsToRooms(rooms.ToList(), json["connections"]);

            return new Game(rooms, player);
        }

        private void SetWalls(Room room)
        {
            for (var y = 0; y <= room.Height; y++)
            {
                for (var x = 0; x <= room.Width; x++)
                {
                    if (y == 0 || y == room.Height) room.AddInteractableTile(new Wall(room, x, y));
                    else if (x == 0 || x == room.Width) room.AddInteractableTile(new Wall(room, x, y));
                }
            }
        }

        private void SetConnectionsToRooms(List<Room> rooms, JToken connectionsJson)
        {
            var connections = CreateConnections(connectionsJson);

            foreach (var room in rooms)
            {
                foreach (var connection in connections)
                {
                    if (connection.BelongsToRoom(room.Id))
                    {
                        int y;
                        int x;
                        switch (connection.GetDirectionByRoom(room.Id))
                        {
                            case WindRose.North:
                                y = 0;
                                x = room.Width / 2;
                                break;
                            case WindRose.East:
                                y = room.Height / 2;
                                x = room.Width;
                                break;
                            case WindRose.South:
                                y = room.Height;
                                x = room.Width / 2;
                                break;
                            case WindRose.West:
                                y = room.Height / 2;
                                x = 0;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        var wall = room.InteractableTiles.Single(w => w.X == x && w.Y == y);
                        room.Remove(wall);

                        room.SetConnection(connection);
                    }
                }
            }
        }

        private IEnumerable<Hallway> CreateConnections(JToken connectionsJson)
        {
            if (!connectionsJson.HasValues)
                throw new ArgumentException("Connections JSON is invalid.");

            var connections = new List<Hallway>();
            foreach (var connection in connectionsJson)
            {
                DoorContext doorContext = null;
                var directions = new Dictionary<WindRose, int>();
                foreach (var child in connection.Children().OfType<JProperty>())
                {
                    if (child.Name.Equals("door"))
                    {
                        var type = (child.First?["type"] ?? "").Value<string>();
                        var color = (child.First?["color"] ?? "").Value<string>();

                        doorContext = new DoorContext(_doorFactory.Create(type, color));
                    }
                    else
                    {
                        directions.Add(Enum.Parse<WindRose>(child.Name, true), child.Value.ToObject<int>());
                    }
                }

                connections.Add(new Hallway(directions, doorContext));
            }

            return connections;
        }

        private IEnumerable<Room> CreateRooms(JToken roomsJson)
        {
            if (!roomsJson.HasValues)
                throw new ArgumentException("Rooms JSON is invalid.");

            var rooms = new List<Room>();

            foreach (var roomJson in roomsJson)
            {
                var id = roomJson["id"]?.Value<int>();
                var width = roomJson["width"]?.Value<int>() + ScaleFactor;
                var height = roomJson["height"]?.Value<int>() + ScaleFactor;
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
            var x = tileJson["x"]?.Value<int>() + ScaleFactor;
            var y = tileJson["y"]?.Value<int>() + ScaleFactor;

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
            var startX = playerJson["startX"]?.Value<int>() + ScaleFactor;
            var startY = playerJson["startY"]?.Value<int>() + ScaleFactor;
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