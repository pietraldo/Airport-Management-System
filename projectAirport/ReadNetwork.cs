using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    using NetworkSourceSimulator;
    internal class ReadNetwork
    {
        private static List<Thing> thingList = new List<Thing>();

        private static Dictionary<string, Factory> factoryFunctions = new Dictionary<string, Factory>()
        {
            { "NCR", new FactoryCrew()},
            { "NPA", new FactoryPassenger()},
            { "NCA", new FactoryCargo()},
            { "NCP", new FactoryCargoPlane()},
            { "NPP", new FactoryPassengerPlane()},
            { "NAI", new FactoryAirport()},
            { "NFL", new FactoryFlight()}
        };

        public static void MessageHandler(object sender, NewDataReadyArgs e)
        {
            Message msg = ((NetworkSourceSimulator)sender).GetMessageAt(e.MessageIndex);

            byte[] msgBytes = msg.MessageBytes;
            string type = Encoding.ASCII.GetString(msgBytes, 0, 3);

            thingList.Add(factoryFunctions[type].makeObjectFromBytes(msgBytes));
        }
        public static void MakeSnapshot()
        {
            // seting name for snapshot
            int h = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;

            string hs = ((h < 10) ? "0" : "") + h.ToString();
            string mins = ((min < 10) ? "0" : "") + min.ToString();
            string secs = ((sec < 10) ? "0" : "") + sec.ToString();

            string snapName = "data/snapshot_" + hs + "_" + mins + "_" + secs + ".json";

            Serialization.SerializeJson(thingList, snapName);
        }

    }
}
