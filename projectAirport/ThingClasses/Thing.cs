using Avalonia.Controls.ApplicationLifetimes;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projectAirport
{
    [JsonDerivedType(typeof(Crew), 1)]
    [JsonDerivedType(typeof(Passenger), 2)]
    [JsonDerivedType(typeof(Cargo), 3)]
    [JsonDerivedType(typeof(PassengerPlane), 4)]
    [JsonDerivedType(typeof(CargoPlane), 5)]
    [JsonDerivedType(typeof(Airport), 6)]
    [JsonDerivedType(typeof(Flight), 7)]
    public abstract class Thing
    {
        protected UInt64 id;
        public UInt64 ID { get { return id; } set { id = value; } }

        public abstract void devideList(ListDivider lsd);
        public Thing(UInt64 id)
        {
            ID = id;
        }

        public void IDUpdateMethod(IDUpdateArgs args)
        {
            if(id==args.ObjectID)
            {
                id=args.NewObjectID;
            }
        }
    }
}

