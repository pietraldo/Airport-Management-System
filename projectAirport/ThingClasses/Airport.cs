using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Airport : Thing, IReportable
    {
        protected string name;
        protected string code;
        protected Single longitude;
        protected Single latitude;
        protected Single amsl;
        protected string country;

        public string Name { get { return name; } set { name = value; } }
        public string Code { get { return code; } set { code = value; } }
        public Single Longitude { get { return longitude; } set { longitude = value; } }
        public Single Latitude { get { return latitude; } set { latitude = value; } }
        public Single Amsl { get { return amsl; } set { amsl = value; } }
        public string Country { get { return country; } set { country = value; } }

        public Airport(UInt64 id, string name, string code, Single longitude, Single latitude, float amls, string country)
            : base(id)
        {
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            Amsl = amsl;
            Country = country;
        }

        public override void devideList(ListDivider lsd) { lsd.AddAirports(this); }
        public string Report(Media media)
        {
            return media.ReportAirport(this);
        }
        public void UpdatePosition(PositionUpdateArgs args)
        {
            if (args.ObjectID != id) return;

            string log_przed = $"Pozycja: ({longitude}, {latitude}, {amsl})";

            longitude = args.Longitude;
            latitude = args.Latitude;
            amsl = args.AMSL;

            string log_po = $"Pozycja: ({longitude}, {latitude}, {amsl})";

            string log = $"Id: {id}, Zmiana pozycji. {log_przed} -> {log_po}";
            DataLogger.LogToFile(log);
        }

        public override string ToString()
        {
            return "{ " + $"{ID}, {Name}, {Code}, " + "{" + $"{Longitude}, {Latitude}" + "}" + $",{amsl}, {country}" + "}";
        }

        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Name":
                    return (true, name, "string");
                case "Code":
                    return (true, code, "string");
                case "WorldPosition":
                    if (fields.Length > 1)
                    {
                        if (fields[1] == "Lat")
                            return (true, latitude.ToString(), "float");
                        else if (fields[1] == "Long")
                            return (true, longitude.ToString(), "float");
                        else
                            return (false, "", "");
                    }
                    else
                        return (true, "{" + $"{longitude}, {latitude}" + "}", "struct");
                case "AMSL":
                    return (true, amsl.ToString(), "float");
                case "CountryCode":
                    return (true, country, "string");
            }

            return (false, "", "");
        }
    }
}
