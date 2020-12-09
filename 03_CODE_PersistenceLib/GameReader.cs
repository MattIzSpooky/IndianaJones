﻿using System;
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

            // This doesn't work!
            var connectedRooms = CreateConnections(json["connections"], rooms.ToList());

            rooms.FirstOrDefault(r => r.Id == startRoomId.Value<int>()).Player = player;

            return new Game(rooms, player);
        }

        private IEnumerable<Room> CreateConnections(JToken connectionsJson, List<Room> rooms)
        {
            if (!connectionsJson.HasValues)
                throw new ArgumentException("Connections JSON is invalid.");

            foreach (var room in rooms)
            {
                Door door = GetDoorByRoomId(connectionsJson, room);
                var roses = GetWindRosesByRoomId(connectionsJson, room);

                foreach (var rose in roses)
                {
                    room.SetConnection(new Connection(room, rose, door));
                }
            }

            return rooms;
        }

        private IEnumerable<WindRose> GetWindRosesByRoomId(JToken connectionsJson, Room room)
        {
            List<WindRose> list = new List<WindRose>();
            foreach (JToken connections in connectionsJson)
            {
                foreach (var child in connections.Children().OfType<JProperty>())
                {
                    if (!child.Name.Equals("door"))
                    {
                        if (child.Value.ToObject<int>() == room.Id) 
                            list.Add(Enum.Parse<WindRose>(child.Name, true).Flip());
                    }
                }
            }

            return list;
        }

        private Door GetDoorByRoomId(JToken connectionsJson, Room room)
        {
            var connectionsDoors = 
                connectionsJson.Children()
                    .Where(child => child["door"] != null)
                    .Where(child => (int)child.First == room.Id)
                    .ToList();
            
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