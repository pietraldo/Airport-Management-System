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
        }

       
        static Dictionary<string, Func<Flight, string, string>> flightsDic = new Dictionary<string, Func<Flight, string, string>>() {
            { "ID", (flight, command) => flight.ID.ToString() },
            { "Origin", (flight, command) => {
                string[] fields=command.Split(".");
                if(fields.Length>1)
                    return airportDic[fields[1]](flight.Origin, string.Join(".",fields.Skip(1).ToArray()));
                else
                    return flight.Origin.ToString();}
            },
            { "Target", (flight, command) => {
                string[] fields=command.Split(".");
                if(fields.Length>1)
                    return airportDic[fields[1]](flight.Target, string.Join(".",fields.Skip(1).ToArray()));
                else
                    return flight.Origin.ToString();}
            },
             { "TakeofTime", (flight, command) => flight.TakeOffTime.ToString() },
             { "LandingTime", (flight, command) => flight.LandingTime.ToString() },
             { "WorldPosition", (flight, command) =>
                {
                    string[] fields=command.Split(".");
                    if(fields.Length>1)
                    {
                        if(fields[1]=="Lat")
                            return flight.Latitude.ToString();
                        else if(fields[1]=="Long")
                            return flight.Longitude.ToString();
                        else
                            return "";
                    }
                    else
                        return "{"+$"{flight.Longitude}, {flight.Latitude}"+"}";
                }
             },
             { "AMSL", (flight, command) => flight.Amls.ToString() },
             { "Plane", (flight, command) => {
                string[] fields=command.Split(".");
                if(fields.Length>1)
                    return planeDic[fields[1]](flight.Plain, string.Join(".",fields.Skip(1).ToArray()));
                else
                    return flight.Plain.ToString();}
            }
        };
        static Dictionary<string, Func<Airport, string, string>> airportDic = new Dictionary<string, Func<Airport, string, string>>() {
            { "id", (airport, command) => airport.ID.ToString() },
            { "name", (airport, command) => airport.Name.ToString() },
            { "amsl", (airport, command) => airport.Amls.ToString() }
        };
        static Dictionary<string, Func<Plane, string, string>> planeDic = new Dictionary<string, Func<Plane, string, string>>() {
            { "ID", (plane, command) => plane.ID.ToString() },
            { "Serial", (plane, command) => plane.Serial.ToString() },
            { "Country", (plane, command) => plane.Country.ToString() },
            { "Model", (plane, command) => plane.Model.ToString() },
        };

        public bool Execute()
        {
            switch (mc.objectClass)
            {
                case "flight":
                    ChooseFieldsToPrintFlight(FilterFlights(data.divider.Flights));
                    break;
            }
            return true;
        }
       
        private List<Flight> FilterFlights(List<Flight> flights)
        {
            {
                //List<Flight> flightsPassed = new List<Flight>();
                //if (mc.conditions != null)
                //{
                //    for (int i = 0; i < flights.Count; i++)
                //    {
                //        bool add = false;
                //        for (int j = 0; j < mc.conditions.Length; j++)
                //        {
                //            bool value = mc.conditions[j].Comparison(flights[i].ID, mc.conditions[j].value);
                //            add = (mc.conditions[j].andOr == "and") ? add && value : add || value;
                //        }
                //        if (add)
                //        {
                //            flightsPassed.Add(flights[i]);
                //        }
                //    }
                //}
                //else
                //{
                //    flightsPassed = flights;
                //}
            }
            return flights;
        }
        private void ChooseFieldsToPrintFlight(List<Flight> flights)
        {
            toPrint = new string[flights.Count, mc.fieldsToDisplay.Length];
            for (int i = 0; i < flights.Count; i++)
            {
                for (int j = 0; j < mc.fieldsToDisplay.Length; j++)
                {
                    string[] fields = mc.fieldsToDisplay[j].Split(".");

                    string objectField = "";

                    switch (mc.objectClass)
                    {
                        case "flight":
                            objectField = flightsDic[fields[0]](flights[i], mc.fieldsToDisplay[j]);
                            break;
                    }

                    toPrint[i, j] = objectField;
                }
            }
        }
    }
}
