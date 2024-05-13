using FlightTrackerGUI;
using projectAirport.DataSources;

namespace projectAirport
{
    internal class FlightSimulator
    {
        private static FlightsGUIData flightsGUI;
        public static bool ShowOnlyFlyingPlanes = false;
        private static List<FlightGUI> lista = new List<FlightGUI>();

        private static WorldPosition outSideTheMap = new WorldPosition(-99999, -99999);

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
                    lock (dataSource)
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
            List<FlightGUI> newList = new List<FlightGUI>();
            
            // adding planes that are flying
            foreach (Flight flight in flights)
                if (flight.UpdatePosition())
                {
                    AdapterFlightGui adp = new AdapterFlightGui(flight);
                    newList.Add(adp);
                }

            // checking which planes are not in the list so do not display it
            for (int i = 0; i < lista.Count(); i++)
            {
                // if list does not contain this flight => we must change it position to be outsidethemap
                // if it is outside the map we don't add it 
                bool was = false;
                for(int j = 0;j<newList.Count();j++)
                {
                    if (lista[i].ID== newList[j].ID)
                    {
                        was = true;
                    }
                }
                if (!was)
                {
                    if (lista[i].WorldPosition.Longitude != outSideTheMap.Longitude)
                    {
                        newList.Add(new FlightGUI {WorldPosition=outSideTheMap, ID = lista[i].ID });
                    }
                }
            }

            lista= newList;

            flightsGUI.UpdateFlights(lista);
            Runner.UpdateGUI(flightsGUI);
        }
    }
}
