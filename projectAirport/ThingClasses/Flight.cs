using DynamicData.Kernel;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Flight : Thing
    {
        protected Airport? origin;
        protected Airport? target;
        protected string takeOffTime;
        protected string landingTime;
        protected Single longitude;
        protected Single latitude;
        protected Single? amls;
        protected UInt64 plainId;
        protected UInt64[] crewId;
        protected UInt64[] loadId;
        public Airport? Origin { get { return origin; } set { origin = value; } }
        public Airport? Target { get { return target; } set { target = value; } }
        public string TakeOffTime { get { return takeOffTime; } set { takeOffTime = value; } }
        public string LandingTime { get { return landingTime; } set { landingTime = value; } }
        public Single Longitude { get { return longitude; } set { longitude = value; } }
        public Single Latitude { get { return latitude; } set { latitude = value; } }
        public Single? Amls { get { return amls; } set { amls = value; } }
        public UInt64 PlainId { get { return plainId; } set { plainId = value; } }
        public UInt64[] CrewId { get { return crewId; } set { crewId = value; } }
        public UInt64[] LoadId { get { return loadId; } set { loadId = value; } }

        public Flight(UInt64 id, Airport? origin, Airport? target, string takeOffTime, string landingTime,
            float? longitude, float? latitude, float? amls, UInt64 plainId, UInt64[] crewId, UInt64[] loadId) : base(id)
        {
            Origin = origin;
            Target = target;
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            if (longitude == null)
                Longitude = origin.Longitude;
            else
                Longitude = (float)longitude;
            if (latitude == null)
                Latitude = origin.Latitude;
            else
                Latitude = (float)latitude;
            Amls = amls;
            PlainId = plainId;
            CrewId = new ulong[crewId.Length];
            for (int i = 0; i < crewId.Length; i++)
                CrewId[i] = crewId[i];
            LoadId = new ulong[loadId.Length];
            for (int i = 0; i < loadId.Length; i++)
                LoadId[i] = loadId[i];
        }
        public override void devideList(ListDivider lsd) { lsd.AddFlights(this); }

        //double currentTime=(DateTime.Now - DateTime.Now.Date).TotalSeconds;
        //float speed = 36;
        public bool UpdatePosition()
        {
            TimeOnly start = new TimeOnly();
            TimeOnly end = new TimeOnly();
            TimeOnly.TryParse(takeOffTime, out start);
            TimeOnly.TryParse(landingTime, out end);

            //currentTime += speed;
            double currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;

            double startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
            double endSec = (end - new TimeOnly(0, 0)).TotalSeconds;

            // if start is later than end
            if (startSec > endSec) endSec += 24 * 3600;

            // plane that don't fly now
            if (!(startSec <= currentTime && endSec >= currentTime))
            {
                // didn't start => show on starting airport
                if(startSec > currentTime)
                {
                    latitude = origin.Latitude;
                    longitude = origin.Longitude;
                }
                else // finished => show on end airport
                {
                    latitude = target.Latitude;
                    longitude = target.Longitude;
                }
                
                return FlightSimulator.ShowOnlyFlyingPlanes;
            }

            
            double timeLeft = endSec-currentTime;


            float distx = target.Longitude - longitude;
            float disty = target.Latitude - latitude;

            longitude += (float)(distx / timeLeft);
            latitude+=(float)(disty / timeLeft);

            return true;
        }

        public void UpdatePosition(PositionUpdateArgs args)
        {
            if (args.ObjectID != id) return;

            longitude = args.Longitude;
            latitude = args.Latitude;
            amls = args.AMSL;

            Console.WriteLine("samolot");
        }
    }
}
