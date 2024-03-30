using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Airport : Thing
    {
        protected string name;
        protected string code;
        protected Single longitude;
        protected Single latitude;
        protected Single amls;
        protected string country;

        public string Name { get { return name; } set { name = value; } }
        public string Code { get { return code; } set { code = value; } }
        public Single Longitude { get { return longitude; } set { longitude = value; } }
        public Single Latitude { get { return latitude; } set { latitude = value; } }
        public Single Amls { get { return amls; } set { amls = value; } }
        public string Country { get { return country; } set { country = value; } }

        public Airport(UInt64 id, string name, string code, Single longitude, Single latitude, float amls, string country)
            : base(id)
        {
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            Amls = amls;
            Country = country;
        }
        public override void devideList(ListDivider lsd) { lsd.AddAirports(this); }
    }
}
