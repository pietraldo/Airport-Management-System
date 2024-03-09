using projectAirport;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;


namespace projectAirport
{
    using NetworkSourceSimulator;

    internal class Program
    {
        private static string pathFileFTR = "data/example_data.ftr";
        private static string pathFileJson = "data/things.json";

        static void WriteMessage(object sender, NewDataReadyArgs e)
        {
            // Write your message here
            Console.WriteLine("Message received. Index: " + e.MessageIndex);
        }


        static void Main(string[] args)
        {
            NetworkSourceSimulator netSim = new NetworkSourceSimulator(pathFileFTR,1000,4000);
            netSim.OnNewDataReady += WriteMessage;
            


            netSim.Run();
            
        }
    }
}

