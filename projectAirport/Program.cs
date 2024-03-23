using projectAirport;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using FlightTrackerGUI;
using System.Xml.Serialization;


namespace projectAirport
{
    using BruTile.Wms;
    using Mapsui.Extensions;
    using Mapsui.Projections;
    using NetTopologySuite.Geometries;
    using NetworkSourceSimulator;
    using System.Data;
    using System.Text;

    internal class Program
    {
        public static void expample()
        {
            FlightsGUIData flightsGUI = new FlightsGUIData();

            int x = 0;
            int y = 0;
            int dirx = 1;
            int diry = -1;
            int vx = 2;
            int vy = 2;
            int rot = 0;
            ulong id = 0;

           
            while (true)
            {
                List<FlightGUI> lista = new List<FlightGUI>();
                FlightGUI flightGUI = new FlightGUI { ID = id, WorldPosition = new WorldPosition(y, x), MapCoordRotation = rot++ % 90 };
                lista.Add(flightGUI);
                (double, double) pos = SphericalMercator.FromLonLat(x, y);



                flightsGUI.UpdateFlights(lista);

                Runner.UpdateGUI(flightsGUI);
                Thread.Sleep(1);

                Random random = new Random();
                x += random.Next() % 5 * dirx;
                y += random.Next() % 5 * diry;

                if (x >= 180)
                    dirx *= -1;
                if (x <= -180)
                    dirx *= -1;

                if (y >= 90)
                    diry *= -1;
                if (y <= -90)
                    diry *= -1;
            }
        }
        private static string pathFileFTR = "data/example_data.ftr";
        private static string pathFileJson = "data/things.json";
        private static List<Thing> thingList = new List<Thing>();

        static void Main(string[] args)
        {
            // Running graphical aplication
            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();

            Thread.Sleep(5);
            // Getting all objects
            List<Thing> things = ReadFile.ConvertToObjects(ReadFile.ReadFileMethod(pathFileFTR));
            
            List<Airport> airportList = new List<Airport>();
            List<Flight> flights = new List<Flight>();
            foreach (Thing thing in things)
            {
                thing.devideList(airportList, flights);
            }



            //flights.Add(new Flight(1,2,3,"17:38", "17:39", 85, 85,0,0,new ulong[0], new ulong[0]));
            //airportList.Add(new Airport(2, "", "", 0, -0, 0, ""));
            //airportList.Add(new Airport(3, "", "", 180, 85f, 0, ""));


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
            float speed = 3600;

            FlightsGUIData flightsGUI = new FlightsGUIData();
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
                currentTime += speed/100;

            }


        }
    }
}

