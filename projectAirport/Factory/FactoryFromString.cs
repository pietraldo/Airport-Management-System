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
        public abstract Thing makeObjectFromString(string[] fields, ListDivider divider);
    }
    class FactoryFromStringCrew : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Crew(ulong.Parse(fields[1]), fields[2],
                ulong.Parse(fields[3]), fields[4], fields[5],
                ushort.Parse(fields[6]), fields[7]);
        }
    }

    class FactoryFromStringPassenger : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Passenger(ulong.Parse(fields[1]), fields[2],
                ulong.Parse(fields[3]), fields[4], fields[5],
                fields[6], ulong.Parse(fields[7]));
        }
    }
    class FactoryFromStringCargo : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 5) throw new Exception("Too short array");

            return new Cargo(ulong.Parse(fields[1]), float.Parse(fields[2], CultureInfo.InvariantCulture),
                fields[3], fields[4]);
        }
    }
    class FactoryFromStringCargoPlane : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 6) throw new Exception("Too short array");

            return new CargoPlane(ulong.Parse(fields[1]), fields[2],
                fields[3], fields[4], float.Parse(fields[5], CultureInfo.InvariantCulture));
        }
    }
    class FactoryFromStringPassengerPlane : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new PassengerPlane(ulong.Parse(fields[1]), fields[2],
                fields[3], fields[4], ushort.Parse(fields[5]), ushort.Parse(fields[6]),
                ushort.Parse(fields[7]));
        }
    }
    class FactoryFromStringAirport : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 8) throw new Exception("Too short array");

            return new Airport(ulong.Parse(fields[1]), fields[2],
                fields[3], float.Parse(fields[4], CultureInfo.InvariantCulture), float.Parse(fields[5], CultureInfo.InvariantCulture),
                float.Parse(fields[6].Replace(".", ",")), fields[7]);
        }
    }

    class FactoryFromStringFlight : FactoryFromString
    {
        public override Thing makeObjectFromString(string[] fields, ListDivider divider)
        {
            if (fields.Length < 12) throw new Exception("Too short array");
            fields[10] = fields[10].Substring(1, fields[10].Length - 2);
            fields[11] = fields[11].Substring(1, fields[11].Length - 2);

            string[] strings1 = fields[10].Split(";");
            string[] strings2 = fields[11].Split(";");
            Crew[] crew = new Crew[strings1.Length];
            Thing[] load = new Thing[strings2.Length];

            for (int i = 0; i < strings1.Length; i++)
            {
                ulong crewId = ulong.Parse(strings1[i]);
                foreach(Crew crew1 in divider.Crews)
                {
                    if(crewId==crew1.ID)
                    {
                        crew[i] = crew1;
                        break;
                    }
                }
            }

            for (int i = 0; i < strings2.Length; i++)
            {
                ulong loadId = ulong.Parse(strings2[i]);
                foreach(Passenger p in divider.Passengers)
                {
                    if(p.ID== loadId)
                        load[i] = p;
                }
                foreach(Cargo p in divider.Cargos)
                {
                    if(p.ID== loadId)
                        load[i] = p;
                }
            }

            Plane plane=null;
            ulong planeId = ulong.Parse(fields[9]);
            foreach (PassengerPlane p in divider.PassengerPlanes)
            {
                if (p.ID == planeId)
                    plane = p;
            }
            foreach (CargoPlane p in divider.CargoPlanes)
            {
                if (p.ID == planeId)
                    plane = p;
            }

            ulong fromId=ulong.Parse(fields[2]);
            ulong toId = ulong.Parse(fields[3]);
            Airport from = null;
            Airport to = null;
            
            foreach(Airport airport in divider.Airports)
            {
                if(airport.ID==fromId)
                    from = airport;
                if(airport.ID==toId)
                    to = airport;
            }

            return new Flight(
                ulong.Parse(fields[1]),
                from,
                to,
                fields[4],
                fields[5],
                float.Parse(fields[6], CultureInfo.InvariantCulture),
                float.Parse(fields[7], CultureInfo.InvariantCulture),
                float.Parse(fields[8], CultureInfo.InvariantCulture),
                plane,
                crew,
                load
            );
        }
    }
}
