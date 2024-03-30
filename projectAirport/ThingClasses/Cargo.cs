using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Cargo(UInt64 id, float weight, string code, string description) : base(id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }
        public override void devideList(ListDivider lsd) { lsd.AddCargos(this); }
    }
}
