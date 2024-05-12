using Avalonia.Controls;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
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
        public void UpdateContactInfo(ContactInfoUpdateArgs args)
        {
            if (args.ObjectID != id) return;

            string log_przed = $"Dane kontaktowe: {phone}, {email}";

            phone = args.PhoneNumber;
            email = args.EmailAddress;

            string log_po = $"Dane kontaktowe: {phone}, {email}";

            string log = $"Id: {id}, Zmiana danych. {log_przed} -> {log_po}";
            DataLogger.LogToFile(log);
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
        public override void devideList(ListDivider lsd) { lsd.AddCrews(this); }
        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Name":
                    return (true, name, "string");
                case "Age":
                    return (true, age.ToString(), "uint");
                case "Phone":
                    return (true, phone, "string");
                case "Email":
                    return (true, email, "string");
                case "Practise":
                    return (true, practice.ToString(), "uint");
                case "Role":
                    return (true, role, "string");
            }

            return (false, "", "");
        }
    }

    public class Passenger : Person
    {
        protected string classe;
        protected UInt64 miles;
        public string Classs { get { return classe; } set { classe = value; } }
        public UInt64 Miles { get { return miles; } set { miles = value; } }

        public Passenger(UInt64 id, string name, ulong age, string phone, string email, string classs, ulong miles) : base(id, name, age, phone, email)
        {
            Classs = classs;
            Miles = miles;
        }
        public override void devideList(ListDivider lsd) { lsd.AddPassengers(this); }

        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Name":
                    return (true, name, "string");
                case "Age":
                    return (true, age.ToString(), "uint");
                case "Phone":
                    return (true, phone, "string");
                case "Email":
                    return (true, email, "string");
                case "Class":
                    return (true, classe, "string");
                case "Miles":
                    return (true, miles.ToString(), "uint");
            }

            return (false, "", "");
        }
    }
}
