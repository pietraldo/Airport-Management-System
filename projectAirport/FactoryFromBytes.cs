using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    abstract class FactoryFromBytes
    {
        public abstract Thing makeObjectFromBytes(byte[] fields);
    }
    class FactoryFromBytesCrew : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            UInt16 nl = BitConverter.ToUInt16(msgBytes, 15);
            string name = Encoding.ASCII.GetString(msgBytes, 17, nl);
            UInt16 age = BitConverter.ToUInt16(msgBytes, 17 + nl);
            string phone = Encoding.ASCII.GetString(msgBytes, 19 + nl, 12);
            UInt16 el = BitConverter.ToUInt16(msgBytes, 31 + nl);
            string email = Encoding.ASCII.GetString(msgBytes, 33 + nl, el);
            UInt16 practise = BitConverter.ToUInt16(msgBytes, 33 + nl + el);
            string role = Encoding.ASCII.GetString(msgBytes, 35 + nl + el, 1);

            return new Crew(id, name, age, phone, email, practise, role);
        }
    }

    class FactoryFromBytesPassenger : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            UInt16 nl = BitConverter.ToUInt16(msgBytes, 15);
            string name = Encoding.ASCII.GetString(msgBytes, 17, nl);
            UInt16 age = BitConverter.ToUInt16(msgBytes, 17 + nl);
            string phone = Encoding.ASCII.GetString(msgBytes, 19 + nl, 12);
            UInt16 el = BitConverter.ToUInt16(msgBytes, 31 + nl);
            string email = Encoding.ASCII.GetString(msgBytes, 33 + nl, el);
            string classe = Encoding.ASCII.GetString(msgBytes, 33 + nl + el, 1);
            UInt64 miles = BitConverter.ToUInt64(msgBytes, 34 + nl + el);

            return new Passenger(id, name, age, phone, email, classe, miles);
        }
    }
    class FactoryFromBytesCargo : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            Single weight = BitConverter.ToSingle(msgBytes, 15);
            string code = Encoding.ASCII.GetString(msgBytes, 19, 6);
            UInt16 dl = BitConverter.ToUInt16(msgBytes, 25);
            string description = Encoding.ASCII.GetString(msgBytes, 27, dl);

            return new Cargo(id, weight, code, description);
        }
    }
    class FactoryFromBytesCargoPlane : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            string serial = Encoding.ASCII.GetString(msgBytes, 15, 10);
            string country = Encoding.ASCII.GetString(msgBytes, 25, 3);
            UInt16 ml = BitConverter.ToUInt16(msgBytes, 28);
            string model = Encoding.ASCII.GetString(msgBytes, 30, ml);
            Single max_load = BitConverter.ToSingle(msgBytes, 30 + ml);

            return new CargoPlane(id, serial, country, model, max_load);
        }
    }
    class FactoryFromBytesPassengerPlane : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            string serial = Encoding.ASCII.GetString(msgBytes, 15, 10);
            string country = Encoding.ASCII.GetString(msgBytes, 25, 3);
            UInt16 ml = BitConverter.ToUInt16(msgBytes, 28);
            string model = Encoding.ASCII.GetString(msgBytes, 30, ml);
            UInt16 firstClass = BitConverter.ToUInt16(msgBytes, 30 + ml);
            UInt16 BuissnessClass = BitConverter.ToUInt16(msgBytes, 32 + ml);
            UInt16 EconomyClass = BitConverter.ToUInt16(msgBytes, 34 + ml);

            return new PassengerPlane(id, serial, country, model, firstClass, BuissnessClass, EconomyClass);
        }

    }
    class FactoryFromBytesAirport : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            UInt16 nl = BitConverter.ToUInt16(msgBytes, 15);
            string name = Encoding.ASCII.GetString(msgBytes, 17, nl);
            string code = Encoding.ASCII.GetString(msgBytes, 17 + nl, 3);
            Single longitude = BitConverter.ToSingle(msgBytes, 20 + nl);
            Single latitude = BitConverter.ToSingle(msgBytes, 24 + nl);
            Single amsl = BitConverter.ToSingle(msgBytes, 28 + nl);
            string country = Encoding.ASCII.GetString(msgBytes, 32 + nl, 3);

            return new Airport(id, name, code, longitude, latitude, amsl, country);
        }
    }

    class FactoryFromBytesFlight : FactoryFromBytes
    {
        public override Thing makeObjectFromBytes(byte[] msgBytes)
        {
            UInt64 id = BitConverter.ToUInt64(msgBytes, 7);
            UInt64 origin = BitConverter.ToUInt64(msgBytes, 15);
            UInt64 target = BitConverter.ToUInt64(msgBytes, 23);
            UInt64 takeoff = BitConverter.ToUInt64(msgBytes, 31);
            UInt64 landing = BitConverter.ToUInt64(msgBytes, 39);
            UInt64 planeId = BitConverter.ToUInt64(msgBytes, 47);
            UInt16 cc = BitConverter.ToUInt16(msgBytes, 55);
            UInt16 pcc = BitConverter.ToUInt16(msgBytes, 57 + 8 * cc);

            DateTime takeoffDateTime = DateTimeOffset.FromUnixTimeMilliseconds((long)takeoff).UtcDateTime;
            DateTime landingTime = DateTimeOffset.FromUnixTimeMilliseconds((long)landing).UtcDateTime;
            string takeoffString = takeoffDateTime.Hour.ToString() + ":" + takeoffDateTime.Minute.ToString();
            string landingString = landingTime.Hour.ToString() + ":" + landingTime.Minute.ToString();

            UInt64[] crew = new UInt64[cc];
            UInt64[] passangers = new UInt64[pcc];

            for (int i = 0; i < cc; i++)
                crew[i] = BitConverter.ToUInt64(msgBytes, 57 + 8 * i);

            for (int i = 0; i < pcc; i++)
                passangers[i] = BitConverter.ToUInt64(msgBytes, 59 + 8 * cc + 8 * i);

            return new Flight(id, origin, target, takeoffString, landingString, null, null, null, planeId, crew, passangers);
        }
    }

}
