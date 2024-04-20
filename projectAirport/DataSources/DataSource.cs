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
        public List<Thing> thingList = new List<Thing>();
        public ListDivider divider = new ListDivider();

        public void FromNetwork(string file_path, int min_time, int max_time) 
        {
            // starting Network Simulator in new thread
            Thread netSim = new Thread(() => {

                // creating network simulator
                NetworkSourceSimulator netSim = new NetworkSourceSimulator(file_path, min_time, max_time);
                
                NetworkEventHandler netSimEventHandler = new NetworkEventHandler(this);
                netSim.OnIDUpdate += netSimEventHandler.IDUpdateHandler;
                netSim.OnPositionUpdate += netSimEventHandler.PositionUpdateHandler;
                netSim.OnContactInfoUpdate += netSimEventHandler.ContactInfoUpdateHandler;

                // adding event handler
                ReadNetwork reader1 = new ReadNetwork(thingList, divider);
                netSim.OnNewDataReady += reader1.MessageHandler;

                // starting serwer thread
                Thread tcpSerwer = new Thread(new ThreadStart(netSim.Run)) { IsBackground = true };
                tcpSerwer.Start();

            });
            netSim.Start();
        }
        public void FromFile(string file_path) 
        {
            thingList = ReadFile.ConvertToObjects(ReadFile.ReadFileMethod(file_path), divider);
        }
        public void MakeSnapshot()
        {
            string dirName = Environment.CurrentDirectory + "/snapshots";
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            string snapName = $"{dirName}/snapshot_{DateTime.Now:HH_mm_ss}.json";
            Console.WriteLine(thingList.Count);
            Serialization.SerializeJson(thingList, snapName);
        }
    }
}
