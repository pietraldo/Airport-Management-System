using FlightTrackerGUI;
using projectAirport.DataSources;

namespace projectAirport
{
    internal class FlightSimulator
    {
        private FlightsGUIData flightsGUI;
        public static bool ShowOnlyFlyingPlanes = false;
        public FlightSimulator()
        {
            // Running graphical interface
            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();

            flightsGUI = new FlightsGUIData();
        }
        public void ShowPlanes(List<Flight> flights, double currentTime)
        {

            

            
            
                List<FlightGUI> lista = new List<FlightGUI>();
                foreach (var flight in flights)
                {
                    if (flight.UpdatePosition(currentTime))
                    {
                        AdapterFlightGui adp = new AdapterFlightGui(flight);
                        lista.Add(adp);
                    }

                }

                Console.WriteLine($"{DateTime.Now.Date.AddSeconds(currentTime).Hour}:{DateTime.Now.Date.AddSeconds(currentTime).Minute}");


                flightsGUI.UpdateFlights(lista);

                Runner.UpdateGUI(flightsGUI);
                

            

        }
    }
}
