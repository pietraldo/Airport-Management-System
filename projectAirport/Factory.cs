using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projectAirport
{
    abstract class Factory
    {
        public abstract Thing makeObjectFromString(string[] fields);
    }

    class FactoryCrew : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Crew(UInt64.Parse(fields[1]), fields[2],
                UInt64.Parse(fields[3]), fields[4], fields[5],
                UInt16.Parse(fields[6]), fields[7]);
        }
    }

    class FactoryPassenger : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Passenger(UInt64.Parse(fields[1]), fields[2],
                UInt64.Parse(fields[3]), fields[4], fields[5],
                fields[6], UInt64.Parse(fields[7]));
        }
    }
    class FactoryCargo : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 5) throw new Exception("Too short array");

            return new Cargo(UInt64.Parse(fields[1]), float.Parse(fields[2].Replace(".", ",")),
                fields[3], fields[4]);
        }
    }
    class FactoryCargoPlane : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 6) throw new Exception("Too short array");

            return new CargoPlane(UInt64.Parse(fields[1]), fields[2],
                fields[3], fields[4], Single.Parse(fields[5].Replace(".", ",")));
        }
    }
    class FactoryPassengerPlane : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new PassengerPlane(UInt64.Parse(fields[1]), fields[2],
                fields[3], fields[4], UInt16.Parse(fields[5]), UInt16.Parse(fields[6]),
                UInt16.Parse(fields[7]));
        }
    }
    class FactoryAirport : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Airport(UInt64.Parse(fields[1]), fields[2],
                fields[3], Single.Parse(fields[4].Replace(".", ",")), Single.Parse(fields[5].Replace(".", ",")),
                Single.Parse(fields[6].Replace(".", ",")), fields[7]);
        }
    }

    class FactoryFlight : Factory
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 12) throw new Exception("Too short array");
            fields[10] = fields[10].Substring(1, fields[10].Length - 2);
            fields[11] = fields[11].Substring(1, fields[11].Length - 2);

            string[] strings1 = fields[10].Split(";");
            string[] strings2 = fields[11].Split(";");
            UInt64[] crewId = new UInt64[strings1.Length];
            UInt64[] loadId = new UInt64[strings2.Length];
            for (int i = 0; i < strings1.Length; i++)
            {
                crewId[i] = UInt64.Parse(strings1[i]);
            }
            for (int i = 0; i < strings2.Length; i++)
            {
                loadId[i] = UInt64.Parse(strings2[i]);
            }

            return new Flight(
                UInt64.Parse(fields[1]),
                UInt64.Parse(fields[2]),
                UInt64.Parse(fields[3]),
                fields[4],
                fields[5],
                Single.Parse(fields[6].Replace(".", ",")),
                Single.Parse(fields[7].Replace(".", ",")),
                Single.Parse(fields[8].Replace(".", ",")),
                UInt64.Parse(fields[9]),
                crewId,
                loadId
            );


        }
    }


}
