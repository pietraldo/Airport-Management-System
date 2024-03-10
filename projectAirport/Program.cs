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
        

        static NetworkSourceSimulator netSim;

        static private Dictionary<string, Factory> factoryFunctions = new Dictionary<string, Factory>()
        {
            { "NCR", new FactoryCrew()},
            { "NPA", new FactoryPassenger()},
            { "NCA", new FactoryCargo()},
            { "NCP", new FactoryCargoPlane()},
            { "NPP", new FactoryPassengerPlane()},
            { "NAI", new FactoryAirport()},
            { "NFL", new FactoryFlight()}
        };
        static List<Thing> thingList = new List<Thing>();

        static void WriteMessage(object sender, NewDataReadyArgs e)
        {
            Message msg= netSim.GetMessageAt(e.MessageIndex);
            
            byte[] msgBytes=msg.MessageBytes;
            string type= Encoding.ASCII.GetString(msgBytes,0,3);

            thingList.Add(factoryFunctions[type].makeObjectFromBytes(msgBytes));
        }

        
        static void Main(string[] args)
        {
            netSim = new NetworkSourceSimulator(pathFileFTR, 1, 1);

            // adding event handler
            netSim.OnNewDataReady += WriteMessage;

            // starting serwer thread
            Thread tcpSerwer = new Thread(new ThreadStart(netSim.Run))
            {
                IsBackground = true
            };
            tcpSerwer.Start();


            string asw = "";
            while ((asw = Console.ReadLine()) != "exit")
            {
                if (asw == "print")
                {
                    int h = DateTime.Now.Hour;
                    int min = DateTime.Now.Minute;
                    int sec = DateTime.Now.Second;

                    string hs = ((h < 10) ? "0" : "")+h.ToString();
                    string mins = ((min < 10) ? "0" : "")+min.ToString();
                    string secs = ((sec < 10) ? "0" : "")+sec.ToString();

                    string snapName = "data/snapshot_"+hs + "_" + mins + "_" + secs + ".json";

                    Serialization.SerializeJson(thingList,  snapName);
                }
            }
        }
    }
}

