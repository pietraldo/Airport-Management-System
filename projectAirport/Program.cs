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

            //Thread planesThread = new Thread(showPlanes);
            //planesThread.Start();
            //// creating network simulator
            //NetworkSourceSimulator netSim = new NetworkSourceSimulator(pathFileFTR, 1, 1);

            //// adding event handler
            //ReadNetwork reader1 = new ReadNetwork(thingList);
            //netSim.OnNewDataReady += reader1.MessageHandler;

            //// starting serwer thread
            //Thread tcpSerwer = new Thread(new ThreadStart(netSim.Run)) { IsBackground = true };
            //tcpSerwer.Start();

            //// reading user commends
            //string asw;
            //while ((asw = Console.ReadLine()) != "exit")
            //{
            //    if (asw == "print")
            //    {
            //        reader1.MakeSnapshot();
            //    }
            //}
            DataSource dataSource = new DataSource();
            dataSource.FromFile();

            ListDivider divider = new ListDivider();
            lock (dataSource.thingList)
            {
                foreach (Thing thing in dataSource.thingList)
                {
                    thing.devideList(divider);
                }
            }

            FlightSimulator flightSimulator = new FlightSimulator();

            
            flightSimulator.ShowPlanes(divider.Flights, divider.Airports);
               
            
            
            
            //while(true)
            //{
            //    ListDivider divider = new ListDivider();
            //    lock (dataSource.thingList)
            //    {
            //        foreach (Thing thing in dataSource.thingList)
            //        {
            //            thing.devideList(divider);
            //        }
            //    }
            //    Console.WriteLine(  );
            //}
            


        }
    }
}

