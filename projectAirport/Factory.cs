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


}
