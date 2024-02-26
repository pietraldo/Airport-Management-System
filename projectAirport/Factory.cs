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


}
