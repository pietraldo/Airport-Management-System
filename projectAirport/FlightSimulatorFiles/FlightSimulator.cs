using FlightTrackerGUI;
using projectAirport.DataSources;

namespace projectAirport
{
    internal class FlightSimulator
    {
        private static FlightsGUIData flightsGUI;
        public static bool ShowOnlyFlyingPlanes = false;
        
        public static void RunGui(DataSource dataSource)
        {
            // Running graphical interface
            Task apkaTask = Task.Run(() => Runner.Run());

            flightsGUI = new FlightsGUIData();

            // updating planes positions
            Thread simulate_planes = new Thread(() =>
            {
                while (true)
                {
                    lock (dataSource.thingList)
                    {
                        ShowPlanes(dataSource.divider.Flights);
                    }
                    Thread.Sleep(1000);
                }
            });
            simulate_planes.IsBackground = true;
            simulate_planes.Start();
        }

        private static void ShowPlanes(List<Flight> flights)
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
