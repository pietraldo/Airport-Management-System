using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    using NetworkSourceSimulator;
    using projectAirport.Factory;
    using System.Data;

    internal class ReadNetwork(List<Thing> thingList)
    {
        private List<Thing> thingList = thingList;
        private static Dictionary<string, FactoryFromBytes> factoryFunctions = new Dictionary<string, FactoryFromBytes>()
        {
            { "NCR", new FactoryFromBytesCrew()},
            { "NPA", new FactoryFromBytesPassenger()},
            { "NCA", new FactoryFromBytesCargo()},
            { "NCP", new FactoryFromBytesCargoPlane()},
            { "NPP", new FactoryFromBytesPassengerPlane()},
            { "NAI", new FactoryFromBytesAirport()},
            { "NFL", new FactoryFromBytesFlight()}
        };

        public void MessageHandler(object sender, NewDataReadyArgs e)
        {
            Message msg = ((NetworkSourceSimulator)sender).GetMessageAt(e.MessageIndex);

            byte[] msgBytes = msg.MessageBytes;
            string type = Encoding.ASCII.GetString(msgBytes, 0, 3);

            thingList.Add(factoryFunctions[type].makeObjectFromBytes(msgBytes));
        }
        public void MakeSnapshot()
        {
            string snapName = $"data/snapshot_{DateTime.Now:HH_mm_ss}.json";

            Serialization.SerializeJson(thingList, snapName);
        }

    }
}
