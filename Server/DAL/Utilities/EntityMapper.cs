using Core.Models;
using Core.Models.Enums.Airport;
using Core.Models.Enums.Flight;
using DAL.Entities;

namespace DAL.Utilities
{
    public class EntityMapper
    {
        public FlightEntity ModelToEntity(Flight flight)
        {
            return new FlightEntity
            {
                Id = flight.Id,
                SerialNumber = flight.SerialNumber,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Status = (int)flight.Status,
                FacilityId = (int)flight.Facility,
            };
        }

        public Flight EntityToModel(FlightEntity flightEntity)
        {
            return new Flight
            {
                Id = flightEntity.Id,
                SerialNumber = flightEntity.SerialNumber,
                ArrivalTime = flightEntity.ArrivalTime,
                DepartureTime = flightEntity.DepartureTime,
                Status = (Status)flightEntity.Status,
                Facility = (Facility)flightEntity.FacilityId,
            };
        }

        public List<Flight> ListEntitiesToModels(List<FlightEntity> flightEntities)
        {
            var flights = new List<Flight>();
            foreach (var entity in flightEntities)
            {
                flights.Add(EntityToModel(entity));
            }
            return flights;
        }
    }
}
