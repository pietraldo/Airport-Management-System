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
            return $"Reporting for {Name}, Ladies and gentelmen, we are at the {airport.Name} airport.";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return $"Reporting for {Name}, Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return $"Reporting for {Name}, Ladies and gentelmen, we are witnessed the {passangerPlane.Serial} takeoff.";
        }
    }
}
