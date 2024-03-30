using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class Newspaper : Media
    {
        public Newspaper(string name) : base(name)
        {
        }
        public override string ReportAirport(Airport airport)
        {
            return "Airport newspaper";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return "Cargo newspaper";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return "Passanger newspaper";
        }
    }
}
