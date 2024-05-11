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
        public ExecuteCommand(MakeCommand mc, DataSource dataSource)
        {
            this.mc = mc;
            data = dataSource;
            Execute();
        }

        static Dictionary<string, Func<Flight, string, string>> flightsDic = new Dictionary<string, Func<Flight, string, string>>() {
            { "id", (flight, command) => flight.ID.ToString() },
            { "amsl", (flight, command) => flight.Amls.ToString() },
            { "origin", (flight, command) => {
                string[] fields=command.Split(".");
                if(fields.Length>1)
                    return airportDic[fields[1]](flight.Origin, string.Join(".",fields.Skip(1).ToArray()));
                else
                    return flight.Origin.ToString();
             }

             }
        };

        static Dictionary<string, Func<Airport, string, string>> airportDic = new Dictionary<string, Func<Airport, string, string>>() {
            { "id", (airport, command) => airport.ID.ToString() },
            { "name", (airport, command) => airport.Name.ToString() },
            { "amsl", (airport, command) => airport.Amls.ToString() }
        };

        private void Execute()
        {

            switch (mc.objectClass)
            {
                case "flight":
                    toPrint = new string[data.divider.Flights.Count, mc.fieldsToDisplay.Length];
                    for (int i = 0; i < data.divider.Flights.Count; i++)
                    {
                        for (int j = 0; j < mc.fieldsToDisplay.Length; j++)
                        {
                            string[] fields = mc.fieldsToDisplay[j].Split(".");
                            toPrint[i, j] = flightsDic[fields[0]](data.divider.Flights[i], mc.fieldsToDisplay[j]);
                        }
                    }
                    break;
            }
        }
        private void ExecuteFlights(List<Flight> flights)
        {
            List<Flight> flightsPassed = new List<Flight>();
            if (mc.conditions != null)
            {
                for (int i = 0; i < flights.Count; i++)
                {
                    bool add = false;
                    for (int j = 0; j < mc.conditions.Length; j++)
                    {
                        bool value = mc.conditions[j].Comparison(flights[i].ID, mc.conditions[j].value);
                        add = (mc.conditions[j].andOr == "and") ? add && value : add || value;
                    }
                    if (add)
                    {
                        flightsPassed.Add(flights[i]);
                    }
                }
            }
            else
            {
                flightsPassed = flights;
            }
        }
    }
}
