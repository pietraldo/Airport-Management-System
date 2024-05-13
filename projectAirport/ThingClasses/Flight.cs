using Avalonia;
using DynamicData.Kernel;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Flight : Thing
    {
        protected Airport? origin;
        protected Airport? target;
        protected string takeOffTime = "";
        protected string landingTime = "";
        protected Single? longitude = 0;
        protected Single? latitude = 0;
        protected Single? amsl=0;
        protected Plane plane;
        protected Crew[] crewId;
        protected Thing[] loadId;

        public Airport? Origin { get { return origin; } set { origin = value; } }
        public Airport? Target { get { return target; } set { target = value; } }
        public string TakeOffTime { get { return takeOffTime; } set { takeOffTime = value; } }
        public string LandingTime { get { return landingTime; } set { landingTime = value; } }
        public Single? Longitude { get { return longitude; } set { longitude = value; } }
        public Single? Latitude { get { return latitude; } set { latitude = value; } }
        public Single? Amsl { get { return amsl; } set { amsl = value; } }
        public Plane Plane { get { return plane; } set { plane = value; } }
        public Crew[] CrewId { get { return crewId; } set { crewId = value; } }
        public Thing[] LoadId { get { return loadId; } set { loadId = value; } }
        

        public Flight() { }
        public Flight(UInt64 id, Airport? origin, Airport? target, string takeOffTime, string landingTime,
                Single? longitude, Single? latitude, Single? amsl, Plane plane, Crew[] crewId, Thing[] loadId) : base(id)
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
            Amsl = amsl;
            Plane = plane;

            CrewId = new Crew[crewId.Length];
            for (int i = 0; i < crewId.Length; i++)
                CrewId[i] = crewId[i];

            LoadId = new Thing[loadId.Length];
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


            float distx = (float)(target.Longitude - longitude);
            float disty =(float)( target.Latitude - latitude);

            
            longitude += (float)(distx / timeLeft);
            latitude+=(float)(disty / timeLeft);

            return true;
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
        
        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch(fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Origin":
                    if (origin == null)
                        return (true, "", "struct");
                    if (fields.Length > 1)
                        return origin.GetFieldAndType(string.Join(".", fields.Skip(1).ToArray()));
                    else
                        return (true, origin.ToString(), "struct");
                case "Target":
                    if (target == null)
                        return (true, "", "struct");
                    if (fields.Length > 1)
                        return target.GetFieldAndType(string.Join(".", fields.Skip(1).ToArray()));
                    else
                        return (true, target.ToString(), "struct");
                case "TakeofTime":
                    return (true, takeOffTime.ToString(), "DateTime");
                case "LandingTime":
                    return (true, landingTime.ToString(), "DateTime");
                case "WorldPosition":
                    if (fields.Length > 1)
                    {
                        if (fields[1] == "Lat")
                            return (true, latitude.ToString(), "float");
                        else if (fields[1] == "Long")
                            return (true, longitude.ToString(), "float");
                        else
                            return (false,"", "");
                    }
                    else
                        return (true, "{" + $"{longitude}, {latitude}" + "}", "struct");
                case "AMSL":
                    return (true, amsl.ToString(), "float");
                case "Plane":
                    if(plane==null)
                        return (true, "", "struct");
                    if (fields.Length > 1)
                        return plane.GetFieldAndType(string.Join(".", fields.Skip(1).ToArray()));
                    else
                        return (true, plane.ToString(), "struct");
            }

            return (false,"","");
        }

        public override bool SetField(string field, string value, DataSource data)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    ID = uint.Parse(value);
                    break;
                case "Origin":
                    if (fields.Length > 1)
                        return origin.SetField(string.Join(".", fields.Skip(1).ToArray()), value, data);
                    else
                    {
                        uint id = uint.Parse(value);
                        for (int i = 0; i < data.divider.Airports.Count(); i++)
                            if (id == data.divider.Airports[i].ID)
                            {
                                Origin = data.divider.Airports[i];
                                return true;
                            }
                    }
                    break;
                case "Target":
                    if (fields.Length > 1)
                        return target.SetField(string.Join(".", fields.Skip(1).ToArray()), value, data);
                    else
                    {
                        uint id = uint.Parse(value);
                        for (int i = 0; i < data.divider.Airports.Count(); i++)
                            if (id == data.divider.Airports[i].ID)
                            {
                                Target = data.divider.Airports[i];
                                return true;
                            }
                    }
                    break;
                case "TakeofTime":
                    TakeOffTime = value;
                    break;
                case "LandingTime":
                    LandingTime= value;
                    break;
                case "WorldPosition":
                    if (fields.Length > 1)
                    {
                        if (fields[1] == "Lat")
                            Latitude = float.Parse(value);
                        else if (fields[1] == "Long")
                            Longitude=float.Parse(value);
                        else
                            return false; 
                    }
                    break;
                case "AMSL":
                    Amsl = float.Parse(value);
                    break;
                case "Plane":
                    if (fields.Length > 1)
                        return plane.SetField(string.Join(".", fields.Skip(1).ToArray()), value, data);
                    else
                    {
                        uint id = uint.Parse(value);
                        for (int i = 0; i < data.divider.PassengerPlanes.Count(); i++)
                            if (id == data.divider.PassengerPlanes[i].ID)
                            {
                                Plane = data.divider.PassengerPlanes[i];
                                return true;
                            }
                        for (int i = 0; i < data.divider.CargoPlanes.Count(); i++)
                            if (id == data.divider.CargoPlanes[i].ID)
                            {
                                Plane = data.divider.CargoPlanes[i];
                                return true;
                            }

                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
