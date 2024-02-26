using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Thing
    {
        public UInt64 ID { get; set; }

        virtual public void UploadFromFile() { }
    }

    [Serializable]
    public class Crew:Thing
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }

        public Crew(){}

        public Crew(UInt64 ID,string name, ulong age, string phone, string email, ushort practice, string role)
        {
            this.ID = ID;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }
        public Crew(string[] fields)
        {
            if (fields.Length < 8) throw new Exception("Too short array");
            ID = UInt64.Parse(fields[1]);
            Name = fields[2];
            Age = UInt64.Parse(fields[3]);
            Phone = fields[4];
            Email = fields[5];
            Practice = UInt16.Parse(fields[6]);
            Role = fields[7];
        }
    }
}

// kazda klasa musi miec: serializer i tworzenie ze stringa