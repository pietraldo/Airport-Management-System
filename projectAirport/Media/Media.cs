using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public abstract class Media(string name)
    {
        string name = name;
        public string Name { get { return name; } }

        public string Report(IReportable reportable)
        {
            return reportable.Report(this);
        }
        public abstract string ReportCargoPlane(CargoPlane cargoPlane);
        public abstract string ReportPassangerPlane(PassengerPlane passangerPlane);
        public abstract string ReportAirport(Airport airport);
    }
}
