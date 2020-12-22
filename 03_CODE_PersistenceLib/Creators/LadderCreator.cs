using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Connections;
using CODE_GameLib.Interactable;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    public class LadderCreator : ICreator<Ladder>
    {
        private readonly IEnumerable<Room> _rooms;
        private readonly InteractableTileFactory _interactableTileFactory = new InteractableTileFactory();

        public LadderCreator(IEnumerable<Room> rooms)
        {
            _rooms = rooms;
        }

        public Ladder Create(JToken jsonToken)
        {
            if (!jsonToken.HasValues)
                throw new ArgumentException("Ladder JSON is invalid.");

            var upperRoomId = jsonToken["UPPER"]?.Value<int>();
            var lowerRoomId = jsonToken["LOWER"]?.Value<int>();

            var upperX = jsonToken["ladder"]["upperX"]?.Value<int>();
            var upperY = jsonToken["ladder"]["upperY"]?.Value<int>();
            var lowerX = jsonToken["ladder"]["lowerX"]?.Value<int>();
            var lowerY = jsonToken["ladder"]["lowerY"]?.Value<int>();

            if (upperRoomId == null ||
                lowerRoomId == null ||
                upperX == null ||
                upperY == null ||
                lowerX == null ||
                lowerY == null)
                throw new NullReferenceException("Rooms JSON is invalid");

            var upperRoom = _rooms.First(r => r.Id == upperRoomId);
            var lowerRoom = _rooms.First(r => r.Id == lowerRoomId);

            var roomBinding = new Dictionary<Room, Room>
            {
                {
                    lowerRoom, upperRoom
                },
                {
                    upperRoom, lowerRoom
                }
            };

            var ladder = new Ladder(roomBinding);

            var upperLadderTile = _interactableTileFactory.Create("ladder", upperRoom, upperX.Value, upperY.Value, ladder);
            var lowerLadderTile = _interactableTileFactory.Create("ladder", lowerRoom, lowerX.Value, lowerY.Value, ladder);
            
            upperRoom.AddInteractable(upperLadderTile);
            lowerRoom.AddInteractable(lowerLadderTile);

            return ladder;
        }

        public IEnumerable<Ladder> CreateMultiple(IEnumerable<JToken> jsonTokens) => jsonTokens.Select(Create).ToList();
    }
}