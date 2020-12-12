using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    internal class RoomCreator
    {
        private readonly InteractableTileFactory _interactableTileFactory;
        private readonly int _scaleFactor;

        public RoomCreator(int scaleFactor)
        {
            _interactableTileFactory = new InteractableTileFactory();
            _scaleFactor = scaleFactor;
        }

        public IEnumerable<Room> Create(JToken roomsJson)
        {
            if (!roomsJson.HasValues)
                throw new ArgumentException("Rooms JSON is invalid.");

            var rooms = new List<Room>();

            foreach (var roomJson in roomsJson)
            {
                var id = roomJson["id"]?.Value<int>();
                var width = roomJson["width"]?.Value<int>() + _scaleFactor;
                var height = roomJson["height"]?.Value<int>() + _scaleFactor;
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

            foreach (var room in rooms)
            {
                SetWalls(room);
            }

            return rooms;
        }

        private InteractableTile CreateInteractableTile(JToken tileJson, Room room)
        {
            var type = tileJson["type"]?.Value<string>();
            var x = tileJson["x"]?.Value<int>() + _scaleFactor;
            var y = tileJson["y"]?.Value<int>() + _scaleFactor;

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
    }
}