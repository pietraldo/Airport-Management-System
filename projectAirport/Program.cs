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

            // creating medias
            List<Media> media = new List<Media>()
            {
                new Television("Telewizja Abelowa"),
                new Television("Kanał TV-tensor"),
                new Radio("Radio Kwantyfikator"),
                new Radio("Radio Shmen"),
                new Newspaper("Gazeta Kategoryczna"),
                new Newspaper("Dziennik Polityczny")
            };


            List<IReportable> reportables = new List<IReportable>();
            reportables.Add(dataSource.divider.Airports.First());
            reportables.Add(dataSource.divider.PassengerPlanes.First());

            NewsGenerator newsGenerator = new NewsGenerator(media, reportables);
            string? news;
            while ((news = newsGenerator.GenerateNextNews()) != null)
            {
                Console.WriteLine(news);
            }



        }
    }
}

