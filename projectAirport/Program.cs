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
    using Microsoft.VisualBasic;
    using NetTopologySuite.Geometries;
    using NetworkSourceSimulator;
    using System.Data;
    using System.Text;

    internal class Program
    {
        static void Main(string[] args)
        {
            // getting data
            DataSource dataSource = new DataSource();
            dataSource.FromFile("data/example_data.ftr");

            // starting simulation
            FlightSimulator flightSimulator = new FlightSimulator();
            while (true)
            {
                lock (dataSource.thingList)
                {
                    flightSimulator.ShowPlanes(dataSource.divider.Flights);
                }
                Thread.Sleep(1000);
            }

        }
    }
}

