using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Connections;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Collectable;
using CODE_PersistenceLib.Creators;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private readonly PlayerCreator _playerCreator = new PlayerCreator();
        private readonly RoomCreator _roomCreator = new RoomCreator();
        private readonly InteractableTileFactory _interactableTileFactory = new InteractableTileFactory();

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

            var rooms =
                InitializeRooms(roomsJson, connectionJson, playerJson["startRoomId"], player);

            var sankaraStoneAmount = rooms.Sum(r =>
                r.Interactables.Count(t => t is SankaraStone));

            return new Game(rooms, player, sankaraStoneAmount);
        }

        private List<Room> InitializeRooms(JToken roomsJson, JToken hallWayJson, JToken startRoomId, Player player)
        {
            var rooms = _roomCreator.CreateMultiple(roomsJson).ToList();

            AddConnections(rooms, hallWayJson);

            var firstRoom = rooms.First(r => r.Id == startRoomId.Value<int>());
            firstRoom.AddInteractable(player);

            return rooms;
        }

        private void AddConnections(IEnumerable<Room> rooms, JToken connectionJson)
        {
            var roomsList = rooms.ToList();
            
            AddHallwaysToRooms(roomsList, connectionJson);
            AddLaddersToRooms(roomsList, connectionJson);
        }

        private void AddLaddersToRooms(IEnumerable<Room> rooms, JToken connectionJson)
        {
            var ladderJson = connectionJson.Where(c => c["ladder"] != null);
            var ladderCreator = new LadderCreator(rooms);
            ladderCreator.CreateMultiple(ladderJson);
        }

        private void AddHallwaysToRooms(IEnumerable<Room> rooms, JToken connectionJson)
        {
            var hallwaysJson = connectionJson.Where(c => c["ladder"] == null);
            var hallwayCreator = new HallwayCreator(rooms);
            var hallways = hallwayCreator.CreateMultiple(hallwaysJson).ToList();

            foreach (var room in rooms)
            {
                foreach (var hallway in hallways.Where(hallWay => hallWay.BelongsToRoom(room.Id)))
                {
                    var (x, y) = CalculateHallwayPosition(hallway, room);

                    var wall = room.Interactables.Single(w => w.X == x && w.Y == y);

                    var interactableHallway = _interactableTileFactory.Create(
                        "hallway", room, x, y, hallway
                    );
                    
                    room.Remove(wall);
                    room.AddHallway(hallway); // TODO: double check if still necessary
                    room.AddInteractable(interactableHallway);
                }
            }
        }

        private (int x, int y) CalculateHallwayPosition(Hallway hallway, Room room)
        {
            int y;
            int x;

            switch (hallway.GetDirectionByRoom(room.Id))
            {
                case Direction.North:
                    y = 0;
                    x = room.Width / 2;
                    break;
                case Direction.East:
                    y = room.Height / 2;
                    x = room.Width;
                    break;
                case Direction.South:
                    y = room.Height;
                    x = room.Width / 2;
                    break;
                case Direction.West:
                    y = room.Height / 2;
                    x = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (x, y);
        }
    }
}