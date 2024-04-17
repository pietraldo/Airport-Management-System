using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class NetworkEventHandler
    {
        private DataSource _dataSource;
        public NetworkEventHandler(DataSource data)
        {
            _dataSource = data;
        }

        public void IDUpdateHandler(object sender, IDUpdateArgs args)
        {
            Console.WriteLine($"Change Id: {args.ObjectID} {args.NewObjectID}");
            
            foreach (var item in _dataSource.thingList)
            {
                lock (item)
                {
                    item.IDUpdateMethod(args);
                }
                
            }
           
        }
        
        public void PositionUpdateHandler(object sender, PositionUpdateArgs args)
        {
            Console.WriteLine($"Position Change: {args.ObjectID}");
            foreach (var item in _dataSource.divider.Flights)
            {
                lock (item)
                {
                    item.UpdatePosition(args);
                }
            }
            foreach (var item in _dataSource.divider.Airports)
            {
                lock (item)
                {
                    item.UpdatePosition(args);
                }
            }
        }

        public void ContactInfoUpdateHandler(object sender, ContactInfoUpdateArgs args)
        {
            Console.WriteLine($"Contact Change: {args.ObjectID}");
            foreach (var item in _dataSource.divider.Passengers)
            {
                lock (item)
                {
                    item.UpdateContactInfo(args);
                }
            }
            foreach (var item in _dataSource.divider.Crews)
            {
                lock (item)
                {
                    item.UpdateContactInfo(args);
                }
            }
        }
    }
}
