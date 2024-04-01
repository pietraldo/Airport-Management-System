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
            return $"{Name} - A report from the {airport.Name} airport {airport.Country}.";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return $"{Name} - An interview with the crew of {cargoPlane.Serial}.";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return $"{Name} - Breaking news! {passangerPlane.Model} aircreaft loses EASA fails certification after inspection of {passangerPlane.Serial}.";
        }
    }
}
