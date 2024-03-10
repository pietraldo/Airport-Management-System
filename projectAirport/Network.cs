using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{

    using NetworkSourceSimulator;
    using System.IO.Enumeration;

    internal class Network
    {
        public static string fileName = "data/example_data.ftr";
        public static int min = 1000;
        public static int max = 1000;

        private static Network instance;

        private static NetworkSourceSimulator netSim;
        private Network()
        {
            netSim = new NetworkSourceSimulator(fileName, min,max);
        }

        public Network getInstance()
        {
            if(instance == null)
            {
                instance= new Network();
            }
            return instance;
        }

        public void RunSimulation()
        {
            Thread tcpSerwer = new Thread(new ThreadStart(netSim.Run))
            {
                IsBackground = true
            };
            tcpSerwer.Start();
        }
    }
}
