using Mapsui.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{

    public class AdapterFlightGui : FlightGUI
    {
        private Flight flight;

        public AdapterFlightGui(Flight flight, Airport start, Airport end)
        {
            this.flight = flight;
            
            ID = flight.ID;
            WorldPosition = new WorldPosition((double)flight.Latitude, (double)flight.Longitude);

            (double, double) aa = SphericalMercator.FromLonLat(start.Longitude, start.Latitude);
            (double, double) bb = SphericalMercator.FromLonLat(end.Longitude, end.Latitude);



            double distX = bb.Item1 - aa.Item1;
            double distY = bb.Item2 - aa.Item2;
            
            MapCoordRotation = Math.Atan2(distX, distY);

            if (!(distY > 0 && distX > 0))
                return;
            
            

            //else if(distY>0 && distX<0)
            //    MapCoordRotation = 3*double.Pi/2- Math.Atan(Math.Abs(distX) / Math.Abs(distY));
            //else if(distY<0 && distX>0)
            //    MapCoordRotation = double.Pi/2- Math.Atan(Math.Abs(distX) / Math.Abs(distY));
            //else //if(distY<0 && distX<0)
            //    MapCoordRotation = 3*double.Pi/2+ Math.Atan(Math.Abs(distX) / Math.Abs(distY));

            //MapCoordRotation = 0.57;
        }

    }
}
