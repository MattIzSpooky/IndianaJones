﻿using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interactable.Doors;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib.Creators
{
    internal class HallWayCreator : ICreator<IEnumerable<Hallway>>
    {
        private readonly DoorFactory _doorFactory;

        public HallWayCreator()
        {
            _doorFactory = new DoorFactory();
        }

        public IEnumerable<Hallway> Create(JToken jsonToken)
        {
            if (!jsonToken.HasValues)
                throw new ArgumentException("Connections JSON is invalid.");

            var hallways = new List<Hallway>();
            foreach (var hallWay in jsonToken)
            {
                DoorContext doorContext = null;
                var directions = new Dictionary<WindRose, int>();
                foreach (var child in hallWay.Children().OfType<JProperty>())
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

                hallways.Add(new Hallway(directions, doorContext));
            }

            return hallways;
        }
    }
}