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
    [JsonDerivedType(typeof(Crew), "Crew")]
    [JsonDerivedType(typeof(Passenger), "Passenger")]
    [JsonDerivedType(typeof(Cargo), "Cargo")]
    [JsonDerivedType(typeof(PassengerPlane), "PassangerPlane")]
    [JsonDerivedType(typeof(CargoPlane), "CargoPlane")]
    [JsonDerivedType(typeof(Airport), "Airport")]
    [JsonDerivedType(typeof(Flight), "Flight")]
    public abstract class Thing
    {
        protected UInt64 id;
        public UInt64 ID { get { return id; } set { id = value; } }

        public Thing(UInt64 id)
        {
            ID = id;
        }
        public Thing(){}

        public abstract void devideList(ListDivider lsd);

        // Function returns if it found that field, value of this field and type
        public abstract (bool, string, string) GetFieldAndType(string field);

        public void IDUpdateMethod(IDUpdateArgs args)
        {
            if(id==args.ObjectID)
            {
                string log_przed = $"Id: {id}";

                id = args.NewObjectID;

                string log_po = $"Id: {id}";

                string log = $"Id: {id}, Zmiana Id. {log_przed} -> {log_po}";
                DataLogger.LogToFile(log);
            }
        }
    }
}

