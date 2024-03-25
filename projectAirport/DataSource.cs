using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    using NetworkSourceSimulator;
    using projectAirport.DataSources;

    public class DataSource
    {
        private static string pathFileFTR = "data/example_data_moje_lotniska.ftr";
        private static string pathFileJson = "data/things.json";
        public List<Thing> thingList = new List<Thing>();

        public void FromNetwork() 
        {
            // starting Network Simulator in new thread
            Thread netSim = new Thread(new ThreadStart(NetworkSimulator));
            netSim.Start();
        }
        public void FromFile() 
        {
            thingList = ReadFile.ConvertToObjects(ReadFile.ReadFileMethod(pathFileFTR));
        }

        private void NetworkSimulator()
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
            string? asw;
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
