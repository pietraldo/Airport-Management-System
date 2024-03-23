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

            (double start_x, double start_y)  = SphericalMercator.FromLonLat(start.Longitude, start.Latitude);
            (double end_x, double end_y) = SphericalMercator.FromLonLat(end.Longitude, end.Latitude);



            double distX = end_x - start_x;
            double distY = end_y - start_y;
            
            MapCoordRotation = Math.Atan2(distX, distY);

            
        }

    }
}
