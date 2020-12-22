using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal class RoomCreator : ICreator<Room>
    {
        private readonly InteractableTileFactory _interactableTileFactory = new InteractableTileFactory();

        public Room Create(JToken roomJson)
        {
            if (!roomJson.HasValues)
                throw new ArgumentException("Rooms JSON is invalid.");

            var id = roomJson["id"]?.Value<int>();
            var width = roomJson["width"]?.Value<int>() - 1; // All of the coordinates in a room starts with 0
            var height = roomJson["height"]?.Value<int>() - 1; // All of the coordinates in a room starts with 0
            var type = roomJson["type"]?.Value<string>();

            if (id == null || width == null || height == null || type == null)
                throw new NullReferenceException("Rooms JSON is invalid");

            if (type != "room")
                throw new Exception($"Room {id.Value} does not have required type 'room'");

            var room = new Room(
                id.Value,
                width.Value,
                height.Value
            );

            var itemsJson = roomJson["items"];
            var specialFloorTiles = roomJson["specialFloorTiles"];

            itemsJson?.Select(json => CreateInteractableTile(json, room))
                .ToList()
                .ForEach(i => room.AddInteractable(i));

            specialFloorTiles?.Select(json => CreateInteractableTile(json, room))
                .ToList()
                .ForEach(i => room.AddInteractable(i));

            SetWalls(room);

            return room;
        }

        public IEnumerable<Room> CreateMultiple(IEnumerable<JToken> jsonTokens) => jsonTokens.Select(Create).ToList();

        private InteractableTile CreateInteractableTile(JToken tileJson, Room room)
        {
            var type = tileJson["type"]?.Value<string>();
            var x = tileJson["x"]?.Value<int>();
            var y = tileJson["y"]?.Value<int>();

            if (type == null || x == null || y == null) throw new NullReferenceException("Item JSON is invalid.");

            var damage = tileJson["damage"]?.Value<string>();
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

        private static void SetWalls(Room room)
        {
            for (var y = 0; y <= room.Height; y++)
            {
                for (var x = 0; x <= room.Width; x++)
                {
                    if (y == 0 || y == room.Height) room.AddInteractable(new Wall(room, x, y));
                    else if (x == 0 || x == room.Width) room.AddInteractable(new Wall(room, x, y));
                }
            }
        }
    }
}