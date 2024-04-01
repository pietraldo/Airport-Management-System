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
            return $"An image of {airport.Name} airport";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return $"An image of {cargoPlane.Model} cargo plane";
        }

        public override string ReportPassangerPlane(PassengerPlane passangerPlane)
        {
            return $"An image of {passangerPlane.Model} passanger plane";
        }
    }
}
