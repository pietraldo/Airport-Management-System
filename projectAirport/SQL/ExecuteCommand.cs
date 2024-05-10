using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class ExecuteCommand
    {
        public MakeCommand mc;
        DataSource data;
        public string[,] toPrint;
        public ExecuteCommand(MakeCommand mc, DataSource dataSource) {
            this.mc = mc;
            data = dataSource;
            Execute();
        }

        private void Execute()
        {
            Dictionary<string, Func<Flight, string>> dict = new Dictionary<string, Func<Flight, string>>() {

                { "id", (flight)=>flight.ID.ToString() },
                { "amsl", (flight)=>flight.Amls.ToString() }
            };
            switch (mc.objectClass)
            {
                case "flight":
                    toPrint = new string[data.divider.Flights.Count, mc.fieldsToDisplay.Length];
                    for(int i=0; i<data.divider.Flights.Count; i++)
                    {
                        for(int j=0; j<mc.fieldsToDisplay.Length; j++)
                        {
                            toPrint[i, j] = dict[mc.fieldsToDisplay[j]](data.divider.Flights[i]);
                        }
                    }
                    break;
            }
        }
        private void ExecuteFlights(List<Flight> flights)
        {
            List<Flight> flightsPassed= new List<Flight>();
            if(mc.conditions!=null)
            {
                for (int i = 0; i < flights.Count; i++)
                {
                    bool add = false;
                    for(int j=0; j< mc.conditions.Length; j++)
                    {
                        bool value = mc.conditions[j].Comparison(flights[i].ID ,mc.conditions[j].value);
                        add = (mc.conditions[j].andOr == "and") ? add && value : add || value;
                    }
                    if(add)
                    {
                        flightsPassed.Add(flights[i]);
                    }
                }
            }
            else
            {
                flightsPassed=flights;
            }
        }
    }
}
