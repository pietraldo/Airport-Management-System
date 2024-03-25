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

            
            DataSource dataSource = new DataSource();
           // dataSource.FromFile("data/bez_lotow.ftr");
            dataSource.FromFile("data/example_data.ftr");

            //dataSource.FromNetwork("data/loty.ftr");

           

            FlightSimulator flightSimulator = new FlightSimulator();

            DateTime currentHour = DateTime.Now.Date;
            double currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;
            float speed = 3600;
            while (true)
            {
                lock(dataSource.thingList)
                {
                    flightSimulator.ShowPlanes(dataSource.divider.Flights, currentTime);
                }
               
                Thread.Sleep(10);
                currentTime += speed / 100;
            }
            
        }
    }
}

