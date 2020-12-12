using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable.Collectable;
using CODE_PersistenceLib.Creators;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private const int ScaleFactor = 1;

        private readonly PlayerCreator _playerCreator = new PlayerCreator(ScaleFactor);
        private readonly RoomCreator _roomCreator = new RoomCreator(ScaleFactor);
        private readonly HallWayCreator _hallWayCreator = new HallWayCreator();

        public Game Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath was null or empty.");

            var json = JObject.Parse(File.ReadAllText(filePath));

            var playerJson = json["player"];
            var roomsJson = json["rooms"];
            var connectionJson = json["connections"];

            if (playerJson == null || roomsJson == null || connectionJson == null)
                throw new NullReferenceException("No json was found!");

            var player = _playerCreator.Create(playerJson);

            var rooms = InitializeRooms(roomsJson, connectionJson, playerJson["startRoomId"], player);

            var sankaraStoneAmount = rooms.Sum(r => r.InteractableTiles.Count(t => t is SankaraStone));

            return new Game(rooms, player, sankaraStoneAmount);
        }

        private List<Room> InitializeRooms(JToken roomsJson, JToken hallWayJson, JToken startRoomId, Player player)
        {
            var rooms = _roomCreator.Create(roomsJson).ToList();

            SetHallWaysToRooms(rooms.ToList(), hallWayJson);

            rooms.First(r => r.Id == startRoomId.Value<int>()).Player = player;

            return rooms;
        }

        private void SetHallWaysToRooms(IEnumerable<Room> rooms, JToken hallWaysJson)
        {
            var hallWays = _hallWayCreator.Create(hallWaysJson).ToList();

            foreach (var room in rooms)
            {
                foreach (var hallWay in hallWays.Where(hallWay => hallWay.BelongsToRoom(room.Id)))
                {
                    int y;
                    int x;

                    switch (hallWay.GetDirectionByRoom(room.Id))
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

                    room.SetConnection(hallWay);
                }
            }
        }
    }
}