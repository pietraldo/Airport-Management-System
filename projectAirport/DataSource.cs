﻿using System;
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
        public List<Thing> thingList = new List<Thing>();
        public ListDivider divider = new ListDivider();

        public void FromNetwork(string file_path) 
        {
            // starting Network Simulator in new thread
            Thread netSim = new Thread(() => NetworkSimulator(file_path));
            netSim.Start();
        }
        public void FromFile(string file_path) 
        {
            thingList = ReadFile.ConvertToObjects(ReadFile.ReadFileMethod(file_path), divider);
        }

        private void NetworkSimulator(string file_path)
        {
            // creating network simulator
            NetworkSourceSimulator netSim = new NetworkSourceSimulator(file_path, 501, 1000);

            // adding event handler
            ReadNetwork reader1 = new ReadNetwork(thingList, divider);
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
