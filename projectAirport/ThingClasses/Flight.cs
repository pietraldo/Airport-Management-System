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
        protected Single? longitude;
        protected Single? latitude;
        protected Single? amls;
        protected UInt64 plainId;
        protected UInt64[] crewId;
        protected UInt64[] loadId;
        public Airport? Origin { get { return origin; } set { origin = value; } }
        public Airport? Target { get { return target; } set { target = value; } }
        public string TakeOffTime { get { return takeOffTime; } set { takeOffTime = value; } }
        public string LandingTime { get { return landingTime; } set { landingTime = value; } }
        public Single? Longitude { get { return longitude; } set { longitude = value; } }
        public Single? Latitude { get { return latitude; } set { latitude = value; } }
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
            Longitude = longitude;
            Latitude = latitude;
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

        public bool UpdatePosition()
        {
            TimeOnly start = new TimeOnly();
            TimeOnly end = new TimeOnly();
            TimeOnly.TryParse(takeOffTime, out start);
            TimeOnly.TryParse(landingTime, out end);

            double currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;

            double startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
            double endSec = (end - new TimeOnly(0, 0)).TotalSeconds;

            // if start is later than end
            if (startSec > endSec) endSec += 24 * 3600;


            // plane don't fly now
            if (!(startSec <= currentTime && endSec >= currentTime))
            {
                // show outside the map
                latitude = 100;
                longitude = 100;
                return FlightSimulator.ShowOnlyFlyingPlanes;
            }

            double duration = endSec - startSec;
            double procTimePassed = (currentTime - startSec) / duration;

            float distx = target.Longitude - origin.Longitude;
            float disty = target.Latitude - origin.Latitude;

            longitude = (float)(origin.Longitude + procTimePassed * distx);
            latitude = (float)(origin.Latitude + procTimePassed * disty);

            return true;
        }
    }
}
