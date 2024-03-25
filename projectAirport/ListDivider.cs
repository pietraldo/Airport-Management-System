using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class ListDivider
    {
        public List<Flight> Flights=new List<Flight>();
        public List<Airport> Airports=new List<Airport>();
        public List<Passenger> Passengers=new List<Passenger>();
        public List<Cargo> Cargos=new List<Cargo>();
        public List<Crew> Crews=new List<Crew>();
        public List<CargoPlane> CargoPlanes=new List<CargoPlane>();
        public List<PassengerPlane> PassengerPlanes=new List<PassengerPlane>();

        public void AddFlights(Flight flight) { Flights.Add(flight); }
        public void AddAirports(Airport airport) { Airports.Add(airport); }
        public void AddPassengers(Passenger passenger) { Passengers.Add(passenger); }
        public void AddCargos(Cargo cargo) { Cargos.Add(cargo); }
        public void AddCrews(Crew crew) { Crews.Add(crew); }
        public void AddCargoPlanes(CargoPlane cargoPlane) { CargoPlanes.Add(cargoPlane); }
        public void AddPassengerPlanes(PassengerPlane passengerPlane) { PassengerPlanes.Add(passengerPlane); }
    }
}
