using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Television : Media
    {
        public Television(string name) : base(name)
        {
        }

        public override string ReportAirport(Airport airport)
        {
            return "Airport television";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return "Cargo television";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return "Passanger television";
        }
    }
}
