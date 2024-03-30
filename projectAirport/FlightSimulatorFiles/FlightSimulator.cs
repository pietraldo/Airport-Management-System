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

        public void ShowPlanes(List<Flight> flights)
        {
            List<FlightGUI> lista = new List<FlightGUI>();

            // adding planes that are flying
            foreach (Flight flight in flights)
                if (flight.UpdatePosition())
                {
                    AdapterFlightGui adp = new AdapterFlightGui(flight);
                    lista.Add(adp);
                }

            flightsGUI.UpdateFlights(lista);
            Runner.UpdateGUI(flightsGUI);
        }
    }
}
