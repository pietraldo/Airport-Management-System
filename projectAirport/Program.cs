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
    using ExCSS;
    using Mapsui.Extensions;
    using Mapsui.Projections;
    using Microsoft.VisualBasic;
    using NetTopologySuite.Geometries;
    using NetworkSourceSimulator;
    using System.Data;
    using System.Linq;
    using System.Text;

    internal class Program
    {
        static void Main(string[] args)
        {
            DataSource dataSource = new DataSource();
            dataSource.FromFile("data/example_data.ftr");
            dataSource.FromNetwork("data/example.ftre",1,1);

            // starting simulation
            FlightSimulator.RunGui(dataSource);


            // creating medias
            List<Media> media = Media.CreateMedia();

            // creating list of reportables
            List<IReportable> reportables = new List<IReportable>();
            reportables = reportables.Concat(dataSource.divider.Airports)
                .Concat(dataSource.divider.CargoPlanes)
                .Concat(dataSource.divider.PassengerPlanes).ToList();


            string asw = "";
            while ((asw = Console.ReadLine()) != "exit")
            {
                if (asw == "print")
                {
                    dataSource.MakeSnapshot();
                }
                if (asw == "raport")
                {
                    NewsGenerator newsGenerator = new NewsGenerator(media, reportables);
                    newsGenerator.PrintAllNews();
                }
            }
            Console.WriteLine("exiting...");
        }
    }
}

