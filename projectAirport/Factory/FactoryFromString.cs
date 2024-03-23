using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.Factory
{
    abstract class FactoryFromString
    {
        public abstract Thing makeObjectFromString(string[] fields);
    }
    class FactoryFromStringCrew : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Crew(ulong.Parse(fields[1]), fields[2],
                ulong.Parse(fields[3]), fields[4], fields[5],
                ushort.Parse(fields[6]), fields[7]);
        }
    }

    class FactoryFromStringPassenger : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Passenger(ulong.Parse(fields[1]), fields[2],
                ulong.Parse(fields[3]), fields[4], fields[5],
                fields[6], ulong.Parse(fields[7]));
        }
    }
    class FactoryFromStringCargo : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 5) throw new Exception("Too short array");

            return new Cargo(ulong.Parse(fields[1]), float.Parse(fields[2], CultureInfo.InvariantCulture),
                fields[3], fields[4]);
        }
    }
    class FactoryFromStringCargoPlane : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 6) throw new Exception("Too short array");

            return new CargoPlane(ulong.Parse(fields[1]), fields[2],
                fields[3], fields[4], float.Parse(fields[5], CultureInfo.InvariantCulture));
        }
    }
    class FactoryFromStringPassengerPlane : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new PassengerPlane(ulong.Parse(fields[1]), fields[2],
                fields[3], fields[4], ushort.Parse(fields[5]), ushort.Parse(fields[6]),
                ushort.Parse(fields[7]));
        }
    }
    class FactoryFromStringAirport : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Airport(ulong.Parse(fields[1]), fields[2],
                fields[3], float.Parse(fields[4], CultureInfo.InvariantCulture), float.Parse(fields[5], CultureInfo.InvariantCulture),
                float.Parse(fields[6].Replace(".", ",")), fields[7]);
        }
    }

    class FactoryFromStringFlight : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields)
        {
            if (fields.Length < 12) throw new Exception("Too short array");
            fields[10] = fields[10].Substring(1, fields[10].Length - 2);
            fields[11] = fields[11].Substring(1, fields[11].Length - 2);

            string[] strings1 = fields[10].Split(";");
            string[] strings2 = fields[11].Split(";");
            ulong[] crewId = new ulong[strings1.Length];
            ulong[] loadId = new ulong[strings2.Length];
            for (int i = 0; i < strings1.Length; i++)
            {
                crewId[i] = ulong.Parse(strings1[i]);
            }
            for (int i = 0; i < strings2.Length; i++)
            {
                loadId[i] = ulong.Parse(strings2[i]);
            }

            return new Flight(
                ulong.Parse(fields[1]),
                ulong.Parse(fields[2]),
                ulong.Parse(fields[3]),
                fields[4],
                fields[5],
                float.Parse(fields[6], CultureInfo.InvariantCulture),
                float.Parse(fields[7], CultureInfo.InvariantCulture),
                float.Parse(fields[8], CultureInfo.InvariantCulture),
                ulong.Parse(fields[9]),
                crewId,
                loadId
            );
        }
    }
}
