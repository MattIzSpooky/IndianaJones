using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Connections;
using CODE_GameLib.Interactable;
using CODE_GameLib.Interactable.Connections;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    /// <summary>
    /// Creates a set of ladders that belong to each other.
    /// </summary>
    public class LadderCreator : ICreator<(Ladder, Ladder)>
    {
        private readonly IEnumerable<Room> _rooms;
        private readonly InteractableTileFactory _interactableTileFactory = new InteractableTileFactory();

        public LadderCreator(IEnumerable<Room> rooms)
        {
            _rooms = rooms;
        }

        public (Ladder, Ladder) Create(JToken jsonToken)
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
                throw new NullReferenceException("Ladder JSON is invalid");

            var upperRoom = _rooms.First(r => r.Id == upperRoomId);
            var lowerRoom = _rooms.First(r => r.Id == lowerRoomId);
            
            var upperLadderTile = (Ladder)_interactableTileFactory.Create("ladder", upperRoom, upperX.Value, upperY.Value, lowerRoom);
            var lowerLadderTile =  (Ladder)_interactableTileFactory.Create("ladder", lowerRoom, lowerX.Value, lowerY.Value, upperRoom);

            upperLadderTile.OtherSide = lowerLadderTile;
            lowerLadderTile.OtherSide = upperLadderTile;
            
            upperRoom.AddInteractable(upperLadderTile);
            lowerRoom.AddInteractable(lowerLadderTile);

            return (lowerLadderTile, upperLadderTile);
        }
        
        public IEnumerable<(Ladder, Ladder)> CreateMultiple(IEnumerable<JToken> jsonTokens) => jsonTokens.Select(Create).ToList();
    }
}