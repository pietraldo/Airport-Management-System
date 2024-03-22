using projectAirport;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;


namespace projectAirport
{
    using NetworkSourceSimulator;
    using System.Text;

    internal class Program
    {
        private static string pathFileFTR = "data/example_data.ftr";
        private static string pathFileJson = "data/things.json";
        private static List<Thing> thingList = new List<Thing>();

        static void Main(string[] args)
        {
            // creating network simulator
            NetworkSourceSimulator netSim = new NetworkSourceSimulator(pathFileFTR, 1, 1);

            // adding event handler
            ReadNetwork reader1 = new ReadNetwork(thingList);
            netSim.OnNewDataReady += reader1.MessageHandler;

            // starting serwer thread
            Thread tcpSerwer = new Thread(new ThreadStart(netSim.Run)) { IsBackground = true };
            tcpSerwer.Start();

            // reading user commends
            string asw;
            while ((asw = Console.ReadLine()) != "exit")
            {
                if (asw == "print")
                {
                    reader1.MakeSnapshot();
                }
            }
        }
    }
}

