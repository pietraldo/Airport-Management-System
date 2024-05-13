using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projectAirport
{
    public class Cargo : Thing
    {
        protected Single weight;
        protected string code;
        protected string description;

        public Single Weight { get { return weight; } set { weight = value; } }
        public string Code { get { return code; } set { code = value; } }
        public string Description { get { return description; } set { description = value; } }
        public Cargo() { }
        public Cargo(UInt64 id, float weight, string code, string description) : base(id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }
        public override void devideList(ListDivider lsd) { lsd.AddCargos(this); }

        public override (bool, string, string) GetFieldAndType(string field)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    return (true, ID.ToString(), "uint");
                case "Weight":
                    return (true, weight.ToString(), "float");
                case "Code":
                    return (true, code, "string");
                case "Description":
                    return (true, description, "string");
            }

            return (false, "", "");
        }
        public override bool SetField(string field, string value, DataSource data)
        {
            string[] fields = field.Split(".");

            switch (fields[0])
            {
                case "ID":
                    ID = uint.Parse(value);
                    break;
                case "Weight":
                    Weight = uint.Parse(value);
                    break;
                case "Code":
                    Code = value;
                    break;
                case "Description":
                    Description = value;
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
