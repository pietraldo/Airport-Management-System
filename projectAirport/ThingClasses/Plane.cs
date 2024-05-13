using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace projectAirport
{
    public abstract class Plane : Thing, IReportable
    {
        protected string serial="";
        protected string country="";
        protected string model = "";
        public string Serial { get { return serial; } set { serial = value; } }
        public string Country { get { return country; } set { country = value; } }
        public string Model { get { return model; } set { model = value; } }

        public Plane() { } 
        public Plane(UInt64 id, string serial, string country, string model) : base(id)
        {
            Serial = serial;
            Country = country;
            Model = model;
        }

        public abstract string Report(Media media);
    }

    public class CargoPlane : Plane
    {
        protected Single maxLoad=0;
        public Single MaxLoad { get { return maxLoad; } set { maxLoad = value; } }

        public CargoPlane() { }
        public CargoPlane(UInt64 id, string serial, string country, string model, Single maxLoad)
            : base(id, serial, country, model)
        {
            MaxLoad = maxLoad;
        }
        public override void devideList(ListDivider lsd) { lsd.AddCargoPlanes(this); }
        public override string Report(Media media)
        {
            return media.ReportCargoPlane(this);
        }
        public override string ToString()
        {
            return "{ " + $"{ID}, {Serial}, {Country},{Model}, {maxLoad}" + "}";
        }
        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Serial":
                    return (true, serial, "string");
                case "CountryCode":
                    return (true, country, "string");
                case "Model":
                    return (true, model, "string");
                case "MaxLoad":
                    return (true, maxLoad.ToString(), "float");
            }

            return (false, "", "");
        }
        public override bool SetField(string field, string value, DataSource data)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    ID = uint.Parse(value);
                    break;
                case "Serial":
                    Serial = value;
                    break;
                case "CountryCode":
                    Country = value;
                    break;
                case "Model":
                    Model = value;
                    break;
                case "MaxLoad":
                    MaxLoad = Single.Parse(value);
                    break;
                default:
                    return false;
            }

            return true;
        }
    }

    public class PassengerPlane : Plane
    {
        protected UInt16 firstClassSize = 0;
        protected UInt16 buisnessClassSize = 0;
        protected UInt16 economyClassSize = 0;

        public UInt16 FirstClassSize { get { return firstClassSize; } set { firstClassSize = value; } }
        public UInt16 BusinessClassSize { get { return buisnessClassSize; } set { buisnessClassSize = value; } }
        public UInt16 EconomyClassSize { get { return economyClassSize; } set { economyClassSize = value; } }

        public PassengerPlane() { }
        public PassengerPlane(UInt64 id, string serial, string country, string model, ushort firstClassSize, ushort buisnessClassSize, ushort economyClassSize)
            : base(id, serial, country, model)
        {
            FirstClassSize = firstClassSize;
            BusinessClassSize = buisnessClassSize;
            EconomyClassSize = economyClassSize;
        }
        public override void devideList(ListDivider lsd) { lsd.AddPassengerPlanes(this); }

        public override string Report(Media media)
        {
            return media.ReportPassangerPlane(this);
        }

        public override string ToString()
        {
            return "{ " + $"{ID}, {Serial}, {Country},{Model}, {firstClassSize}, {buisnessClassSize}, {economyClassSize}" + "}";
        }

        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Serial":
                    return (true, serial, "string");
                case "CountryCode":
                    return (true, country, "string");
                case "Model":
                    return (true, model, "string");
                case "FirstClassSize":
                    return (true, firstClassSize.ToString(), "uint");
                case "BusinessClassSize":
                    return (true, buisnessClassSize.ToString(), "uint");
                case "EconomyClassSize":
                    return (true, economyClassSize.ToString(), "uint");
            }

            return (false, "", "");
        }
        public override  bool SetField(string field, string value, DataSource data)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    ID = uint.Parse(value);
                    break;
                case "Serial":
                    Serial = value;
                    break;
                case "CountryCode":
                    Country = value;
                    break;
                case "Model":
                    Model = value;
                    break;
                case "FirstClassSize":
                    FirstClassSize = UInt16.Parse(value);
                    break;
                case "BusinessClassSize":
                    BusinessClassSize = UInt16.Parse(value);
                    break;
                case "EconomyClassSize":
                    EconomyClassSize = UInt16.Parse(value);
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
