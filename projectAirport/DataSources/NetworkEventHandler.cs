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
            lock (_dataSource)
            {
                foreach (var item in _dataSource.thingList)
                {
                    if (item.ID == args.ObjectID)
                    {
                        item.IDUpdateMethod(args);
                        return;
                    }
                }
            }
            DataLogger.LogToFile($"Operacja UpdateId na objekcie {args.ObjectID} nie możliwa do zrealizowania");
        }

        public void PositionUpdateHandler(object sender, PositionUpdateArgs args)
        {
            lock (_dataSource)
            {
                foreach (var item in _dataSource.divider.Flights)
                {
                    if (item.ID == args.ObjectID)
                    {
                        item.UpdatePosition(args);
                        return;
                    }
                }
                foreach (var item in _dataSource.divider.Airports)
                {
                    if (item.ID == args.ObjectID)
                    {
                        item.UpdatePosition(args);
                        return;
                    }
                }
            }
            DataLogger.LogToFile($"Operacja UpdatePosition na objekcie {args.ObjectID} nie możliwa do zrealizowania");
        }

        public void ContactInfoUpdateHandler(object sender, ContactInfoUpdateArgs args)
        {
            lock ( _dataSource)
            {
                foreach (var item in _dataSource.divider.Passengers)
                {
                    if (item.ID == args.ObjectID)
                    {
                        item.UpdateContactInfo(args);
                        return;
                    }


                }
                foreach (var item in _dataSource.divider.Crews)
                {
                    if (item.ID == args.ObjectID)
                    {
                        item.UpdateContactInfo(args);
                        return;
                    }


                }
            }
            
            DataLogger.LogToFile($"Operacja UpdateContactInfo na objekcie {args.ObjectID} nie możliwa do zrealizowania");
        }
    }
}
