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
        public static List<Media> CreateMedia()
        {
            return new List<Media>()
            {
                new Television("Telewizja Abelowa"),
                new Television("Kanał TV-tensor"),
                new Radio("Radio Kwantyfikator"),
                new Radio("Radio Shmen"),
                new Newspaper("Gazeta Kategoryczna"),
                new Newspaper("Dziennik Polityczny")
            };
        }

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
