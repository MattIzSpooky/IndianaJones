using System.Collections.Generic;

namespace CODE_GameLib.Connections
{
    public class Ladder
    {
        /// <summary>
        /// Contains the room bindings.
        /// Lets say we have:
        /// UPPER 14
        /// LOWER 1.
        ///
        /// If you you enter 1, you should go to 14.
        /// If you enter 14, you go to 1.
        ///
        /// It is saved like: Room1, Room14, Room14, Room1
        /// </summary>
        private readonly Dictionary<Room, Room> _roomBindings;

        public Ladder(Dictionary<Room, Room> roomBindings)
        {
            _roomBindings = roomBindings;
        }

        /// <summary>
        /// Climb the ladder from a room and return the next room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Room Climb(Room room) => _roomBindings[room];
    }
}