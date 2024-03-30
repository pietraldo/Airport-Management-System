using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public abstract class Plane : Thing, IReportable
    {
        protected string serial;
        protected string country;
        protected string model;
        public string Serial { get { return serial; } set { serial = value; } }
        public string Country { get { return country; } set { country = value; } }
        public string Model { get { return model; } set { model = value; } }

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
        protected Single maxLoad;
        public Single MaxLoad { get { return maxLoad; } set { maxLoad = value; } }

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
    }

    public class PassengerPlane : Plane
    {
        protected UInt16 firstClassSize;
        protected UInt16 buisnessClassSize;
        protected UInt16 economyClassSize;

        public UInt16 FirstClassSize { get { return firstClassSize; } set { firstClassSize = value; } }
        public UInt16 BuisnessClassSize { get { return buisnessClassSize; } set { buisnessClassSize = value; } }
        public UInt16 EconomyClassSize { get { return economyClassSize; } set { economyClassSize = value; } }

        public PassengerPlane(UInt64 id, string serial, string country, string model, ushort firstClassSize, ushort buisnessClassSize, ushort economyClassSize)
            : base(id, serial, country, model)
        {
            FirstClassSize = firstClassSize;
            BuisnessClassSize = buisnessClassSize;
            EconomyClassSize = economyClassSize;
        }
        public override void devideList(ListDivider lsd) { lsd.AddPassengerPlanes(this); }

        public override string Report(Media media)
        {
            return media.ReportPassangerPlane(this);
        }
    }
}
