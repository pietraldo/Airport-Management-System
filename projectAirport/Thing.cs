﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectAirport
{
    [JsonDerivedType(typeof(Crew), 1)]
    [JsonDerivedType(typeof(Passenger), 2)]
    [JsonDerivedType(typeof(Cargo), 3)]
    [JsonDerivedType(typeof(PassengerPlane), 4)]
    [JsonDerivedType(typeof(CargoPlane), 5)]
    public abstract class Thing
    {
        protected UInt64 id;
        public UInt64 ID { get { return id; }  set { id = value; } }
        public Thing(UInt64 id) 
        {
            ID= id;
        }
    }
   
    public abstract class Person:Thing
    {
        protected string name;
        protected string phone;
        protected UInt64 age;
        protected string email;
        
        public string Name { get { return name; } set { name = value; } }
        public UInt64 Age { get { return age; } set { age = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }

        public Person(UInt64 id, string name, ulong age, string phone, string email):base(id)
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }
    }
    
    public class Crew: Person
    {
        protected UInt16 practice;
        protected string role;
        public UInt16 Practice { get { return practice; }  set { practice = value; } }
        public string Role { get { return role; } set { role = value; } }

        public Crew(UInt64 id, string name, ulong age, string phone, string email, ushort practice, string role):base(id, name,age, phone, email)
        {
            Practice = practice;
            Role = role;
        }
    }

    public class Passenger: Person
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

    public class Cargo:Thing
    {
        protected Single weight;
        protected string code;
        protected string description;

        public Single Weight { get { return weight; } set { weight= value; } }
        public string Code { get { return code; } set { code= value; } }
        public string Description { get { return description; } set { description= value; } }

        public Cargo(UInt64 id, float weight, string code, string description):base(id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }
    }
    public class Plane:Thing
    {
        protected string serial;
        protected string country;
        protected string model;
        public string Serial { get { return serial; } set { serial = value;} }
        public string Country { get { return country; } set { country = value; } }
        public string Model { get { return model; }  set { model = value;} }

        public Plane(UInt64 id, string serial, string country, string model):base(id)
        {
            Serial = serial;
            Country = country;
            Model = model;
        }
    }

    public class CargoPlane:Plane
    {
        protected Single maxLoad;
        public Single MaxLoad { get { return maxLoad;  } set { maxLoad = value; } }

        public CargoPlane(UInt64 id, string serial, string country, string model,Single maxLoad)
            :base(id,serial, country, model)
        {
            MaxLoad = maxLoad;
        }
    }

    public class PassengerPlane : Plane
    {
        protected UInt16 firstClassSize;
        protected UInt16 buisnessClassSize;
        protected UInt16 economyClassSize;
        
        public UInt16 FirstClassSize { get{ return firstClassSize; } set { firstClassSize = value; } }
        public UInt16 BuisnessClassSize { get{ return buisnessClassSize; } set { buisnessClassSize = value; } }
        public UInt16 EconomyClassSize { get{ return economyClassSize; } set { economyClassSize = value; } }

        public PassengerPlane(UInt64 id, string serial, string country, string model, ushort firstClassSize, ushort buisnessClassSize, ushort economyClassSize) 
            : base(id,serial, country, model)
        {
            FirstClassSize = firstClassSize;
            BuisnessClassSize = buisnessClassSize;
            EconomyClassSize = economyClassSize;
        }
    }
}

