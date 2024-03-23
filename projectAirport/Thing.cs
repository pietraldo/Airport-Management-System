﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projectAirport
{
    [JsonDerivedType(typeof(Crew), 1)]
    [JsonDerivedType(typeof(Passenger), 2)]
    [JsonDerivedType(typeof(Cargo), 3)]
    [JsonDerivedType(typeof(PassengerPlane), 4)]
    [JsonDerivedType(typeof(CargoPlane), 5)]
    [JsonDerivedType(typeof(Airport), 6)]
    [JsonDerivedType(typeof(Flight), 7)]
    public abstract class Thing
    {
        protected UInt64 id;
        public UInt64 ID { get { return id; } set { id = value; } }

        public virtual void devideList(List<Airport> air, List<Flight> fl) { }
        public Thing(UInt64 id)
        {
            ID = id;
        }
    }

    public abstract class Person : Thing
    {
        protected string name;
        protected string phone;
        protected UInt64 age;
        protected string email;

        public string Name { get { return name; } set { name = value; } }
        public UInt64 Age { get { return age; } set { age = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }

        public Person(UInt64 id, string name, ulong age, string phone, string email) : base(id)
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }
    }

    public class Crew : Person
    {
        protected UInt16 practice;
        protected string role;
        public UInt16 Practice { get { return practice; } set { practice = value; } }
        public string Role { get { return role; } set { role = value; } }

        public Crew(UInt64 id, string name, ulong age, string phone, string email, ushort practice, string role) : base(id, name, age, phone, email)
        {
            Practice = practice;
            Role = role;
        }
    }

    public class Passenger : Person
    {
        protected string classe;
        protected UInt64 miles;
        public string Class { get { return classe; } set { classe = value; } }
        public UInt64 Miles { get { return miles; } set { miles = value; } }

        public Passenger(UInt64 id, string name, ulong age, string phone, string email, string classe, ulong miles) : base(id, name, age, phone, email)
        {
            Class = classe;
            Miles = miles;
        }
    }

    public class Cargo : Thing
    {
        protected Single weight;
        protected string code;
        protected string description;

        public Single Weight { get { return weight; } set { weight = value; } }
        public string Code { get { return code; } set { code = value; } }
        public string Description { get { return description; } set { description = value; } }

        public Cargo(UInt64 id, float weight, string code, string description) : base(id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }
    }
    public class Plane : Thing
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
    }

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
        public override void devideList(List<Airport> air, List<Flight> fl) { air.Add(this); }
    }

    public class Flight : Thing
    {
        protected UInt64 origin;
        protected UInt64 target;
        protected string takeOffTime;
        protected string landingTime;
        protected Single? longitude;
        protected Single? latitude;
        protected Single? amls;
        protected UInt64 plainId;
        protected UInt64[] crewId;
        protected UInt64[] loadId;
        public UInt64 Origin { get { return origin; } set { origin = value; } }
        public UInt64 Target { get { return target; } set { target = value; } }
        public string TakeOffTime { get { return takeOffTime; } set { takeOffTime = value; } }
        public string LandingTime { get { return landingTime; } set { landingTime = value; } }
        public Single? Longitude { get { return longitude; } set { longitude = value; } }
        public Single? Latitude { get { return latitude; } set { latitude = value; } }
        public Single? Amls { get { return amls; } set { amls = value; } }
        public UInt64 PlainId { get { return plainId; } set { plainId = value; } }
        public UInt64[] CrewId { get { return crewId; } set { crewId = value; } }
        public UInt64[] LoadId { get { return loadId; } set { loadId = value; } }

        public Flight(UInt64 id, UInt64 origin, UInt64 target, string takeOffTime, string landingTime,
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
        public override void devideList(List<Airport> air, List<Flight> fl) { fl.Add(this); }

        public bool UpdatePosition(Airport airStart, Airport airEnd,double currentTime)
        {
            TimeOnly start = new TimeOnly();
            TimeOnly end = new TimeOnly();
            TimeOnly.TryParse(takeOffTime, out start);
            TimeOnly.TryParse(landingTime, out end);


            double startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
            double endSec = (end - new TimeOnly(0, 0)).TotalSeconds;
            //double currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;

            if (startSec > endSec) endSec += 24 * 3600;

            double duration = startSec - endSec;




            // plane don't fly now
            if (!(startSec <= currentTime && endSec >= currentTime))
            {
                latitude = 100;
                return false;
            }

            double procTimePassed = (currentTime - startSec) / duration;

            float distx = airStart.Longitude - airEnd.Longitude;
            float disty = airStart.Latitude - airEnd.Latitude;

            longitude = (float)(airStart.Longitude + procTimePassed * distx);
            latitude = (float)(airStart.Latitude + procTimePassed * disty);

            return true;
        }
    }
}

