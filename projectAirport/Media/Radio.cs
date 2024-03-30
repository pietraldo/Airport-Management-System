using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Radio : Media
    {
        public Radio(string name) : base(name)
        {
        }
        public override string ReportAirport(Airport airport)
        {
            return "Airport radio";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return "Cargo radio";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return "Passanger radio";
        }
    }
}
