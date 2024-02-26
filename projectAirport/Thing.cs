using System;
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
    public abstract class Thing
    {
        protected UInt64 id;
        public UInt64 ID { get { return id; }  set { id = value; } }
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

        protected Person() { }
        public Person(UInt64 id, string name, ulong age, string phone, string email)
        {
            ID = id;
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

        public Crew() { }

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

        public Passenger() { }

        public Passenger(UInt64 id, string name, ulong age, string phone, string email, string classe, ulong miles) : base(id, name, age, phone, email)
        {
            Class = classe;
            Miles = miles;
        }
    }
}

