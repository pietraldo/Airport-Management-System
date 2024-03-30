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

            Television abelowa = new Television("Abelowa");
            Radio shmen = new Radio("Shmen");

            List<Media> media = new List<Media>();
            media.Add(abelowa);
            media.Add(shmen);

            List<IReportable> reportables = new List<IReportable>();
            reportables.Add(dataSource.divider.Airports.First());
            reportables.Add(dataSource.divider.PassengerPlanes.First());

            foreach (Media mediaItem in media)
            {
                foreach(IReportable ir in reportables)
                {
                    Console.WriteLine(mediaItem.Report(ir));
                }
            }

        }
    }
}

