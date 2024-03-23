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

            float distY = end.Latitude - start.Latitude;
            float distX= end.Longitude - start.Longitude;

            //if (!(distY > 0 && distX > 0))
            //    return;
            MapCoordRotation = double.Pi/2- Math.Atan(distX/distY);
            WorldPosition = new WorldPosition((double)flight.Latitude, (double)flight.Longitude);

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
