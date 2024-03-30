using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class NewsGenerator(List<Media> medias, List<IReportable> reportables)
    {
        private List<Media> _media=medias;
        private List<IReportable> _reportabl=reportables;
       
        public string GenerateNextNews()
        {
            return "";
        }
    }
}
