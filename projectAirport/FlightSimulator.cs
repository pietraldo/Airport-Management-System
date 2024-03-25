using FlightTrackerGUI;
using projectAirport.DataSources;

namespace projectAirport
{
    internal class FlightSimulator
    {
        private FlightsGUIData flightsGUI;
        public FlightSimulator()
        {
            // Running graphical interface
            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();

            flightsGUI = new FlightsGUIData();
        }
        public void ShowPlanes(List<Flight> flights, List<Airport> airportList)
        {
         

            List<(Flight, (Airport, Airport))> ff = new List<(Flight, (Airport, Airport))>();
            foreach (Flight flight in flights)
            {
                Airport from = null;
                Airport to = null;
                foreach (Airport airport in airportList)
                {
                    if (flight.Origin == airport.ID)
                    { from = airport; }
                    if (flight.Target == airport.ID)
                    { to = airport; }
                }
                ff.Add((flight, (from, to)));
            }
            DateTime currentHour = DateTime.Now.Date;
            double currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;
            float speed = 2000;

            
            while (true)
            {
                List<FlightGUI> lista = new List<FlightGUI>();
                foreach (var flight in ff)
                {
                    if (flight.Item1.UpdatePosition(flight.Item2.Item1, flight.Item2.Item2, currentTime))
                    {
                        AdapterFlightGui adp = new AdapterFlightGui(flight.Item1, flight.Item2.Item1, flight.Item2.Item2);
                        lista.Add(adp);
                    }

                }

                Console.WriteLine($"{DateTime.Now.Date.AddSeconds(currentTime).Hour}:{DateTime.Now.Date.AddSeconds(currentTime).Minute}");


                flightsGUI.UpdateFlights(lista);

                Runner.UpdateGUI(flightsGUI);
                Thread.Sleep(10);
                currentTime += speed / 100;

            }

        }
    }
}
